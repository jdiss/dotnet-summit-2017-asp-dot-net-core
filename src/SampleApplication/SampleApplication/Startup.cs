using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using SampleApplication.Filters;
using SampleApplication.Middlewares;
using SampleApplication.Models;
using SampleApplication.Models.Entities;
using SampleApplication.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace SampleApplication
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            // For inMemory configuration we use a singleton to not lose our data 
            services.AddSingleton<ICustomerService, InMemoryCustomerService>();

            // Service using EntityFramework
            // If you're using this, take a look at the bottom of the file and reactive the database initializer as well
            //var dbSettings = Configuration.GetSection("Database");
            //var driver = dbSettings.GetValue<string>("Driver", "mssql");
            //services.AddDbContext<SampleApplicationDbContext>(options =>
            //    {
            //        if (driver == "postgres")
            //        {
            //            options.UseNpgsql(Configuration.GetConnectionString("SampleApplicationPostgres"));
            //            return;
            //        }

            //        options.UseSqlServer(Configuration.GetConnectionString("SampleApplication"));
            //    }
            //);
            //services.AddScoped<ICustomerService, DatabaseCustomerService>();

            // Add framework services.
            services.AddMvc(options =>
            {
                options.Filters.Add(new ExceptionFilter());
            });
            services.AddAutoMapper(mapper => mapper.AddProfile<EntityMappingProfile>());
            services.AddTransient<DatabaseInitializer>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info()
                {
                    Title = "Awesome Customer API",
                    Version = "1.0"
                });

                var filePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(filePath, "SampleApplication.xml");
                options.IncludeXmlComments(xmlPath);
            });
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.Map("/api", appBuilder =>
            {
                appBuilder.UseMiddleware<AuthenticationMiddleware>();

                appBuilder.UseMvc(routes =>
                {
                    routes.MapRoute("api", "{controller}");
                });

                appBuilder.UseSwagger();
                appBuilder.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("v1/swagger.json", "API v1");
                });
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "app",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //var dbInitializer = app.ApplicationServices.GetService<DatabaseInitializer>();
            //dbInitializer.Migrate();
        }
    }
}