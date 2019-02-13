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
using CleanArch.Infrastructure.ShipaFreightComponents;
using CleanArch.Core.Repository.SalesForce;
using CleanArch.Core.Repository.Communication;
using CleanArch.Infrastructure.CommunicationRepository;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using CleanArch.Core.Utilities;
using FluentValidation.AspNetCore;
using FluentValidation;
using CleanArch.Core.Model;
using CleanArch.Core.ModelValidator;

namespace CleanArch.Api
{
    public class Startup
    {
        public static IConfiguration Config { get; private set; }
        public IConfiguration Configuration { get; private set; }
        //public IConfigurationRoot Configuration { get; set; }
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            //  Configuration = configuration;           

            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
           
        }

      //  public IConfiguration Configuration { get; } 
        

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //connection            
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddAutoMapper();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Validators
            services.AddSingleton<IValidator<SignupModel>, SignupModelValidator>();
            // Validators
            // override modelstate
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(p => p.ErrorMessage)).ToList();
                    var result = new
                    {
                        Code = "101",
                        Message = "Validation errors",
                        Errors = errors
                    };
                    return new BadRequestObjectResult(result);
                };
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddFluentValidation();
            services.AddOptions();
           
            // Create an Autofac Container and push the framework services
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);

            // Register your own services within Autofac
            containerBuilder.RegisterType<OneSignal>().As<OneSignal>();
            containerBuilder.RegisterType<QuotationService>().As<IQuotationService>();
            containerBuilder.RegisterType<QuotationDataRepository>().As<IQuotationRepository>();
            containerBuilder.RegisterType<UserManagementService>().As<IUserManagementService>();
            containerBuilder.RegisterType<UserManagementRepository>().As<IUserManagementRepository>();
            containerBuilder.RegisterType<SalesForcePushObject>().As<ISalesForcePushObject>();
            containerBuilder.RegisterType<MailService>().As<ICommunicationService>();

            
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
