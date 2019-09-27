using CoreSolution.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.EntityFrameworkCore
{
    public  class EfCoreDbContext : DbContext
    {
        public EfCoreDbContext()
        #region 对应于Shop_api 的start_up配置
        //DbContextOptions<EfCoreDbContext> options: base(options)
        #endregion
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                DbContextConfigurer.Configure(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        private DbSet<Userinfo_t> Userinfo_t { get; set; }


    }
}
