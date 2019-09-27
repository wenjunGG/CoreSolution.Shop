using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CoreSolution.AutoMapper.Startup;
using CoreSolution.IService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CoreSolution.Shop.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            #region EF 数据库迁徙 放在后台去写了
            //var connection = "Data Source=192.168.1.44\\sqlserver;Initial Catalog=SkyJun;Persist Security Info=True;User ID=sa;Password=reload"; //Configuration.GetConnectionString("");
            // services.AddDbContext<EntityFrameworkCore.EfCoreDbContext>(options =>options.UseSqlServer(connection, b => b.MigrationsAssembly("CoreSolution.EntityFrameworkCore"));
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region Autofac容器
            var builder = new ContainerBuilder();//实例化Autofac容器
            builder.Populate(services);

            var assemblys = Assembly.Load("CoreSolution.Service");
            var baseType = typeof(IServiceSupport);

            builder.RegisterAssemblyTypes(assemblys)
                .Where(i => baseType.IsAssignableFrom(i) && i != baseType)
                .AsImplementedInterfaces();
            var container = builder.Build();

            #endregion

            return new AutofacServiceProvider(container);//让Autofac接管core内置DI容器
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region AutoMapper配置项
            AutoMapperStartup.Register();//加载AutoMapper配置项
            #endregion


            app.UseMvc();
        }
    }
}
