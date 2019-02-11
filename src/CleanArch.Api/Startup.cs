using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CleanArch.Core.Interface.Quotation;
using CleanArch.Core.Service.Quotation;
using CleanArch.Core.Repository.Quotation;
using CleanArch.Infrastructure.DataRepository.Quotation;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CleanArch.Infrastructure.Core;
using AutoMapper;
using CleanArch.Core.Interface.UserManagement;
using CleanArch.Core.Service.UserManagement;
using CleanArch.Infrastructure.DataRepository.UserManagement;
using CleanArch.Core.Repository.UserManagement;

namespace CleanArch.Api
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
            //connection            
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddAutoMapper();
            services.AddMvc();           
            // Create an Autofac Container and push the framework services
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);

            // Register your own services within Autofac
            containerBuilder.RegisterType<QuotationService>().As<IQuotationService>();
            containerBuilder.RegisterType<QuotationDataRepository>().As<IQuotationRepository>();
            containerBuilder.RegisterType<UserManagementService>().As<IUserManagementService>();
            containerBuilder.RegisterType<UserManagementRepository>().As<IUserManagementRepository>();

            // Build the container and return an IServiceProvider from Autofac
            var container = containerBuilder.Build();
            return container.Resolve<IServiceProvider>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
