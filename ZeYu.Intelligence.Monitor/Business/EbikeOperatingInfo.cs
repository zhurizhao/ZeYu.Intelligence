using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using ZeYu.Intelligence.Monitor.Data;

namespace ZeYu.Intelligence.Monitor.Business
{
    public class EbikeOperatingInfo
    {
        public static int Add(Model.EbikeOperatingInfo model)
        {
            int result = 0;
            using (var context = new DefaultDbContext())
            {

                context.EbikeOperatingInfo.Add(model);

                result = context.SaveChanges();
            }
            return result;
        }

        public static int AddBatch(List< Model.EbikeOperatingInfo> list)
        {
            int result = 0;
            using (var context = new DefaultDbContext())
            {
                foreach (Model.EbikeOperatingInfo model in list)
                {
                    context.EbikeOperatingInfo.Add(model);
                }

                result = context.SaveChanges();
            }
            return result;
        }

        public static void ImportToDB()
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
                //JObject jobj = JObject.Parse(body);
                //SaveInfo
                Model.EbikeOperatingInfo ebikeOperInfo= JsonConvert.DeserializeObject<Model.EbikeOperatingInfo>(body);
                Add(ebikeOperInfo);

                if (ebikeOperInfo.AlarmNO != 0)
                {
                    Model.Alarm alarm = new Model.Alarm();
                    alarm.AlarmNO = ebikeOperInfo.AlarmNO;
                    alarm.Parms = ebikeOperInfo.AlarmParm;
                    alarm.SamplingTime = ebikeOperInfo.SamplingTime;
                    alarm.TerminalID = ebikeOperInfo.TerminalID;
                    alarm.EventID = ebikeOperInfo.EventID;

                    Business.Alarm.Add(alarm);
                }

                channel.BasicAck(ea.DeliveryTag, false);
            };
            String consumerTag = channel.BasicConsume("EBikeInfoToDB", false, consumer);
        }
    }
}
