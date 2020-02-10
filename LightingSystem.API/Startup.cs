using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightingSystem.Data.Repositories;
using LightingSystem.Domain.Commands;
using LightingSystem.Domain.Queries;
using LightingSystem.Infrastructure.Database;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LightingSystem.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMediatR(typeof(Startup));
            services.AddMediatR(typeof(GetLightingSystemQuery));
            services.AddMediatR(typeof(AddLightSystemCommand));
            services.AddDbContext<HomeLightSystemContext>(options =>
               options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
               b => b.MigrationsAssembly("LightingSystem.API")));
            services.AddScoped<IHomeLightSystemRepository, HomeLightSystemRepository>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
