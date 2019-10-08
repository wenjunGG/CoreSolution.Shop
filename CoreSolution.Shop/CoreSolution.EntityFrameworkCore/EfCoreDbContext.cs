using CoreSolution.Domain;
using CoreSolution.Domain.Entity;
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
        #region 基础表
        private DbSet<UserInfo> UserInfo { get; set; }
        private DbSet<Menu> Menu { get; set; }

        private DbSet<MenuRole> MenuRole { get; set; }
        private DbSet<Role> Role { get; set; }
        private DbSet<UserRole> UserRole { get; set; }
        #endregion
    }
}
