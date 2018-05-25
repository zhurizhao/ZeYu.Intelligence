using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZeYu.Intelligence.WebAPI
{

    

    public class AppSettings
    {
        public static string RedisConnection = "";
    }

    /// <summary>
    /// Redis
    /// </summary>
    public class Authentication
    {
        /// <summary>
        /// 签发者
        /// </summary>
        public bool Issuer { get; set; }
        /// <summary>
        /// 签名Key
        /// </summary>
        public bool SigningKey { get; set; }
        
    }
}
