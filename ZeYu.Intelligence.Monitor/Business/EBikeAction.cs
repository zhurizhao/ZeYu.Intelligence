using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using ZeYu.Intelligence.Monitor.Data;
using System.Linq;
using System.Data.SqlClient;

namespace ZeYu.Intelligence.Monitor.Business
{
    public class EBikeAction
    {
        public static void Analyze()
        {
            ConnectionFactory factory = new ConnectionFactory();

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
                ZeYu.Intelligence.Monitor.Business.EBikeAction.Update(jobj);
                channel.BasicAck(ea.DeliveryTag, false);
            };
            String consumerTag = channel.BasicConsume("EBikeInfo", false, consumer);
        }

        public static void Update(JObject jobj)
        {
            long terminalID = (long)jobj["TerminalID"];
            string eventID = jobj["EventID"].ToString();
            string preEventID = jobj.GetValue("PreEventID")==null?"": jobj["PreEventID"].ToString();
            long samplingTime = (long)jobj["SamplingTime"];
            double staticVoltageA = Math.Round((double)jobj["StaticVoltageA"],2);
            int status = (int)jobj["Status"];

            Model.EbikeOperatingInfo operInfo = new Model.EbikeOperatingInfo();
            operInfo.TerminalID = terminalID;
            operInfo.EventID = eventID;
            operInfo.PreEventID = preEventID;
            operInfo.SamplingTime = samplingTime;
            operInfo.Voltage = staticVoltageA;
            operInfo.Status = status;


            //充电开始
            if (status == 4 || status == 8)
            {
                var charge = new Model.Charge();
                Business.Charge.CreateModel(operInfo, ref charge);

                using (var context = new DefaultDbContext())
                {
                    context.Charge.Add(charge);
                    context.SaveChanges();

                    if (!eventID.Equals(preEventID) && status == 8)
                    {
                        operInfo.EventID = operInfo.PreEventID; //修改事件ID为上一次事件ID
                        Business.Ride.UpdateOrCreate(operInfo);
                    }
                }

            }
            // 骑行开始
            else if (status == 1 || status == 7)
            {
                Model.Ride ride = new Model.Ride();

                Business.Ride.CreateModel(operInfo, ref ride);

                using (var context = new DefaultDbContext())
                {
                    context.Ride.Add(ride);
                    context.SaveChanges();

                    if (!eventID.Equals(preEventID) && status == 7)
                    {
                        //暂时不分析停车，因为停车时长暂时不主要分析
                        operInfo.EventID = operInfo.PreEventID; //修改事件ID为上一次事件ID
                        Business.Charge.UpdateOrCreate(operInfo);
                    }

                }
            }
            //更新充电时长
            else if (status == 5)
            {
                Business.Charge.UpdateOrCreate(operInfo);
            }
            //更新骑行时长
            else if (status == 2)
            {
                Business.Ride.UpdateOrCreate(operInfo);
            }
            //骑行结束
            else if (status == 3)
            {
                operInfo.EventID = operInfo.PreEventID;
                Business.Ride.UpdateOrCreate(operInfo);
            }
            //骑行结束
            else if (status == 6)
            {
                operInfo.EventID = operInfo.PreEventID;
                Business.Charge.UpdateOrCreate(operInfo);
            }
        }
    }
}
