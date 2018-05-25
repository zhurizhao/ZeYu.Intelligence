using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions;
using ZeYu.Intelligence.Monitor.Model;
//using Microsoft.Extensions.Configuration;


namespace ZeYu.Intelligence.Monitor.Data
{
    public class MySqlDbContext : DbContext
    {
        string connectString = string.Empty;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            connectString = COCOPASS.Helper.NetCoreHelper.ConfigurationManager.GetConnectString("MySqlDbConnection");
            optionsBuilder.UseMySQL(connectString);
            
        }

    }
}
