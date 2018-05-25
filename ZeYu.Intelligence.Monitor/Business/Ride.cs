using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZeYu.Intelligence.Monitor.Data;

namespace ZeYu.Intelligence.Monitor.Business
{
    public class Ride
    {
        public static void CreateModel(Model.EbikeOperatingInfo operInfo, ref Model.Ride ride)
        {
            ride.TerminalID = operInfo.TerminalID;
            ride.EventID = operInfo.EventID;
            ride.StartTime = operInfo.SamplingTime;
            ride.EndTime = operInfo.SamplingTime + 30; //默认至少骑行30S
            ride.Duration = 30;
        }

        public static void UpdateModel(Model.EbikeOperatingInfo operInfo, ref Model.Ride ride)
        {
            ride.EndTime = operInfo.SamplingTime;
            ride.Duration = (int)(operInfo.SamplingTime - ride.StartTime);
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
                var ride = context.Ride.FirstOrDefault(c => c.EventID == operInfo.EventID);
                if (ride == null)
                {
                    ride = new Model.Ride();
                    Business.Ride.CreateModel(operInfo, ref ride);

                    context.Ride.Add(ride);
                }
                else //更新充电时长
                {
                    Business.Ride.UpdateModel(operInfo, ref ride);
                    context.Ride.Update(ride);
                }
                result = context.SaveChanges();
            }
            return result;
        }

    }
}
