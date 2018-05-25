using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZeYu.Intelligence.Monitor.Data;

namespace ZeYu.Intelligence.Monitor.Business
{
    public class Charge
    {
        public static void CreateModel(Model.EbikeOperatingInfo operInfo, ref Model.Charge charge)
        {
            charge.Duration = 30;
            charge.EndTime = operInfo.SamplingTime + 30;
            charge.StartTime = operInfo.SamplingTime;
            charge.EndVoltage = operInfo.Voltage;
            charge.StartVoltage = operInfo.Voltage;
            charge.EventID = operInfo.EventID;
            charge.TerminalID = operInfo.TerminalID;
        }

        public static void UpdateModel(Model.EbikeOperatingInfo operInfo, ref Model.Charge charge)
        {
            charge.EndTime = operInfo.SamplingTime;
            charge.EndVoltage = operInfo.Voltage;
            charge.Duration = (int)(operInfo.SamplingTime - charge.StartTime);
        }

        /// <summary>
        /// 注意此处的EventID,需要在调用时判断好
        /// </summary>
        /// <param name="operInfo"></param>
        /// <returns></returns>
        public static int UpdateOrCreate(Model.EbikeOperatingInfo operInfo)
        {
            int result = 0;
            using (var context = new DefaultDbContext())
            {
                var charge = context.Charge.FirstOrDefault(c => c.EventID == operInfo.EventID);
                if (charge == null)
                {
                    charge = new Model.Charge();
                    Business.Charge.CreateModel(operInfo, ref charge);

                    context.Charge.Add(charge);
                }
                else //更新充电时长
                {
                    Business.Charge.UpdateModel(operInfo, ref charge);
                    context.Charge.Update(charge);
                }
                result=context.SaveChanges();
            }
            return result;
        }
    }
}
