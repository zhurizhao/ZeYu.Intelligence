using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeYu.Intelligence.WebAPI.Models;

namespace ZeYu.Intelligence.WebAPI.Data
{
    public class DBStarCloudContext : DbContext
    {
        public DBStarCloudContext(DbContextOptions<DBStarCloudContext> opt)
            : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<QueryResult> QueryResult { get; set; }
    }
}
