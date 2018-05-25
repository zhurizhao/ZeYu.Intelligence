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
    public class DefaultDbContext : DbContext
    {
        string connectString = string.Empty;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            connectString = COCOPASS.Helper.NetCoreHelper.ConfigurationManager.GetConnectString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectString);
            
        }



        public DbSet<Model.EbikeOperatingInfo> EbikeOperatingInfo { get; set; }
        public DbSet<Model.ActiveInfo> ActiveInfo { get; set; }
        public DbSet<Model.Ride> Ride { get; set; }
        public DbSet<Model.Charge> Charge { get; set; }
        public DbSet<Model.Alarm> Alarm { get; set; }
        

    }
}
