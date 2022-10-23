using AWM.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AWMWfm.Model
{
    public class AWMDBContext: DbContext
    {
        public AWMDBContext()
        {

        }
        public AWMDBContext(DbContextOptions<AWMDBContext> options)
             : base(options)
        {
        }
        public DbSet<Concern> Concerns { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ConcernType> ConcernTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PlatformSettings> PlatformSettings { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("AWMDBConnectionString");
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
