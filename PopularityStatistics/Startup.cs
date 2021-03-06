﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PopularityStatistics.Data;
using PopularityStatistics.Infrastucture;
using ReportDataBase.Models;
using Report = PopularityStatistics.Models.Report;

namespace PopularityStatistics
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IUniqueKeyService, UniqueNameService>()
                .AddSingleton<TaskPool<Report>>()
                .AddSingleton<IFileRepository, CloudStorageAdapter>()
                .AddSingleton<ReportContext>()
                //.
                .AddSingleton<IReportRepository, ReportRepository>()
                .AddSingleton<TaskRepository>()
                .AddMvc();
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
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
