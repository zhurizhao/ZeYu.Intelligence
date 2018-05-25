using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace COCOPASS.Helper.NetCoreHelper
{
    public class ConfigurationManager
    {
        static IConfigurationRoot configuration;
        static ConfigurationManager()
        {
            configuration = GetConfiguration();
        }
        public static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var Config = builder.Build();

            return Config;
        }

        public static string GetConfigValue(string key)
        {
            return configuration[key];
        }
        public static string GetConnectString(string key)
        {
            return configuration.GetConnectionString(key);
        }
    }
}
