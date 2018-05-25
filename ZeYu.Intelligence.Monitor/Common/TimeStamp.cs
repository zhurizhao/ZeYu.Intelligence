using System;
using System.Collections.Generic;
using System.Text;

namespace ZeYu.Intelligence.Monitor.Common
{
    public class TimeStamp
    {
        /// <summary>
        /// 获取1970-01-01至dateTime的毫秒数
        /// </summary>
        //public long GetTimestamp(DateTime dateTime)
        //{
        //    DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        //    return (dateTime.Ticks - dt.Ticks) / 10000;
        //}

        /// <summary>
        /// 根据时间戳timestamp（单位毫秒）计算日期
        /// </summary>
        //public DateTime NewDate(long timestamp)
        //{
        //    DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        //    long tt = dt.Ticks + timestamp * 10000;
        //    return new DateTime(tt);
        //}

        public static long GetTimestamp(DateTime dateTime)
        {
            return (dateTime.Ticks - 621355968000000000) / 10000;
        }

        public static DateTime NewDate(long timestamp)
        {
            long tt = 621355968000000000 + timestamp * 10000;
            return new DateTime(tt);
        }
    }
}
