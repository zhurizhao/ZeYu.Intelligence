using System;
using System.Collections.Generic;
using System.Text;
using ZeYu.Intelligence.Monitor.Data;

namespace ZeYu.Intelligence.Monitor.Business
{
    public class Alarm
    {
        public static int Add(Model.Alarm model)
        {
            int result = 0;
            using (var context = new DefaultDbContext())
            {

                context.Alarm.Add(model);

                result = context.SaveChanges();
            }
            return result;
        }

        public static int AddBatch(List<Model.Alarm> list)
        {
            int result = 0;
            using (var context = new DefaultDbContext())
            {
                foreach(Model.Alarm model in list)
                {
                    context.Alarm.Add(model);
                }

                result = context.SaveChanges();
            }
            return result;
        }
    }
}
