using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using ProcessEngine.Maps;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessEngine.EF
{
   public class DBContext:DbContext
    {
        public static readonly LoggerFactory LoggerFactory =
       new LoggerFactory(new[] { new DebugLoggerProvider() });
        public DBContext(DbContextOptions<DBContext> options)
           : base(options)
        {
        }
        //public DbSet<WorkFlow> workFlows { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(LoggerFactory);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WorkFlowMap());
            modelBuilder.ApplyConfiguration(new WorkFlowNodeMap());
        }
    }
}
