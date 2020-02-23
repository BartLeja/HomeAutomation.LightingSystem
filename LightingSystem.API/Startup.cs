using AutoMapper;
using LightingSystem.API.Commands;
using LightingSystem.API.Queries;
using LightingSystem.Data.Dapper;
using LightingSystem.Data.EntityConfigurations;
using LightingSystem.Data.Mappers;
using LightingSystem.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddMediatR(typeof(AddLightPointCommand));
            services.AddMediatR(typeof(DisableLighPointCommand));
            services.AddMediatR(typeof(DisableAllLightPointsCommand));
            services.AddMediatR(typeof(EnableAllLightPointsCommand));
            services.AddMediatR(typeof(ChangeLightBulbStatusCommand));
      
            services.AddDbContext<HomeLightSystemContext>(options =>
               options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
               b => b.MigrationsAssembly("LightingSystem.API")));
            services.AddScoped<IHomeLightSystemRepository, HomeLightSystemRepository>();
            services.AddTransient<ISqlConnectionFactory>
                (x => new SqlConnectionFactory("Server=localhost; Port = 5432; User ID=postgres; Password=Sim13vetson!; Database=HomeLightSystem;"));

            services.AddAutoMapper(typeof(Startup), typeof(HomeLightSystemMapper));
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
