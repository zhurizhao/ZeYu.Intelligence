using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZeYu.Intelligence.WebAPI.Common
{
    public class Time
    {
        public static long GetTimestamp(DateTime dateTime)
        {
            return (dateTime.Ticks - 621355968000000000) / 10000;
        }

        /// <summary>
        /// 返回UTC毫秒
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long GetUTCTimestamp(DateTime dateTime)
        {
            return (dateTime.ToUniversalTime().Ticks - 621355968000000000) / 10000;
        }
    }
}
