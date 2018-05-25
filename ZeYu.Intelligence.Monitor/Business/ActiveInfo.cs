using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using ZeYu.Intelligence.Monitor.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions;
using System.Linq;
using ZeYu.Intelligence.Monitor.Data;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Data.SqlClient;

namespace ZeYu.Intelligence.Monitor.Business
{
    public class ActiveInfo
    {
        /// <summary>
        /// 从队列获取数据标记到数据库设备在线情况
        /// </summary>
        public static void Calculate()
        {
            ConnectionFactory factory = new ConnectionFactory();
            // "guest"/"guest" by default, limited to localhost connections
            //factory.UserName = "administrator";
            //factory.Password = "administrator";
            //factory.VirtualHost = "/";
            //factory.HostName = "api.lyiot.top";
            //factory.Port = 5673;

            factory.Uri = new Uri(Config.RabbitMQConnectString);

            //在20断开WIFI后20S左右还是会退出程序
            factory.AutomaticRecoveryEnabled = true;
            factory.NetworkRecoveryInterval = TimeSpan.FromSeconds(10);



            IConnection conn = factory.CreateConnection();

            IModel channel = conn.CreateModel();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                var body = Encoding.UTF8.GetString(ea.Body);
                JObject jobj = JObject.Parse(body);
                ZeYu.Intelligence.Monitor.Business.ActiveInfo.Update(jobj);
                channel.BasicAck(ea.DeliveryTag, false);
            };
            String consumerTag = channel.BasicConsume("OnlineMonitor", false, consumer);
        }

        /// <summary>
        /// 分析统计并标记离线，因为离线后不会有数据必须用后台计算得出
        /// 在线的标记在方法Calculate（计算）里会标记
        /// </summary>
        public static void AnalyzeOffLine(object state)
        {
            using (var context = new DefaultDbContext())
            {
                long nowticks = ZeYu.Intelligence.Monitor.Common.TimeStamp.GetTimestamp(DateTime.UtcNow) / 1000;
                SqlParameter parm = new SqlParameter("NowTicks", nowticks);
                var list1 = context.Set<ZeYu.Intelligence.Monitor.Model.ActiveInfo>().FromSql($"select * from ActiveInfo where ABS(@NowTicks-LatestSamplingTime)>300 ", parm).ToList();
                var list2 = context.Set<ZeYu.Intelligence.Monitor.Model.ActiveInfo>().FromSql($"select * from ActiveInfo where ABS(@NowTicks-LatestSamplingTime)<301 ", parm).ToList();
                UpdateTerminalOnlineSummary(list1,list2);

                //Console.WriteLine(list1.Count);
            }

        }


        public static void UpdateTerminalOnlineSummary(List<Model.ActiveInfo> list1, List<Model.ActiveInfo> list2)
        {
            long offline = list1.Count;
            long online = list2.Count;

            using (var context = new DefaultDbContext())
            {
                context.Database.ExecuteSqlCommand($"update TerminalOnlineSummary set OnLine=@OnLine,OffLine=@OffLine", new[]
                {
                    new SqlParameter("OnLine", online),
                    new SqlParameter("OffLine", offline),
                });


                if (offline > 0)
                {
                    StringBuilder terminalIds = new StringBuilder();
                    foreach (Model.ActiveInfo ai in list1)
                    {
                        terminalIds.Append(",");
                        terminalIds.Append(ai.TerminalID);
                    }

                    context.Database.ExecuteSqlCommand($"UPDATE ActiveInfo set OnLine=0 where TerminalID in (" + terminalIds.ToString().Substring(1) + ")");
                }
            }
        }

        public static int Update(JObject jobj)
        {
            int effects = 0;

            ZeYu.Intelligence.Monitor.Model.ActiveInfo ai = new ZeYu.Intelligence.Monitor.Model.ActiveInfo();

            ai.TerminalID = (long)jobj.GetValue("TerminalID");
            ai.LatestSamplingTime = (long)jobj.GetValue("SamplingTime");
            ai.DataType = jobj.GetValue("DataType").ToString();
            ai.UpdateTime = DateTime.Now;

            long nowticks = ZeYu.Intelligence.Monitor.Common.TimeStamp.GetTimestamp(DateTime.UtcNow) / 1000;

            long tmp = System.Math.Abs(nowticks - ai.LatestSamplingTime);
            if (tmp < 300)
                ai.Online = true;

            using (var context = new DefaultDbContext())
            {
                var item = context.ActiveInfo.FirstOrDefault(m => m.TerminalID == ai.TerminalID);
                if (item == null)
                {
                    context.ActiveInfo.Add(ai);
                }
                else
                {
                    //Your context already includes the entity, so rather that creating a new one, 
                    //get the existing one based on the ID of the entity and update its properties, 
                    //then save it
                    if (item.LatestSamplingTime < ai.LatestSamplingTime)
                    {
                        item.LatestSamplingTime = ai.LatestSamplingTime;
                        item.Online = ai.Online;
                        item.UpdateTime = ai.UpdateTime;
                        item.DataType = ai.DataType;
                        context.ActiveInfo.Update(item);
                    }
                }

                effects = context.SaveChanges();
            }

            return effects;
        }
    }
}

