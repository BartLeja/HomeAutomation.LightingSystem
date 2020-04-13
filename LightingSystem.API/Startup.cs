using AutoMapper;
using LightingSystem.API.Features.HomeLightingSystem.Create;
using LightingSystem.API.Features.HomeLightingSystem.DisableAllLightPoints;
using LightingSystem.API.Features.HomeLightingSystem.EnableAllLightPoints;
using LightingSystem.API.Features.HomeLightingSystem.HomeLightSystemDataQuery;
using LightingSystem.API.Features.LightPoint.ChangeLightBulbStatus;
using LightingSystem.API.Features.LightPoint.Create;
using LightingSystem.API.Features.LightPoint.Disable;
using LightingSystem.API.Features.LightPoint.LightPointDataQuery;
using LightingSystem.API.Hubs;
using LightingSystem.Data.Dapper;
using LightingSystem.Data.EntityConfigurations;
using LightingSystem.Data.Mappers;
using LightingSystem.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using System.Threading.Tasks;

namespace LightingSystem.API
{
    public class Startup
    {
        // We use a key generated on this server during startup to secure our tokens.
        // This means that if the app restarts, existing tokens become invalid. It also won't work
        // when using multiple servers.
        public static SymmetricSecurityKey SecurityKey;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var guid = Encoding.ASCII.GetBytes(Configuration.GetSection("AuthenticationGuid").Value);
            SecurityKey = new SymmetricSecurityKey(guid);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMediatR(typeof(Startup));
            services.AddMediatR(typeof(GetLightingSystemByIdQuery));
            services.AddMediatR(typeof(GetLightPointQuery));
            services.AddMediatR(typeof(AddLightSystemCommand));
            services.AddMediatR(typeof(AddLightPointCommand));
            services.AddMediatR(typeof(DisableLighPointCommand));
            services.AddMediatR(typeof(DisableAllLightPointsCommand));
            services.AddMediatR(typeof(EnableAllLightPointsCommand));
            services.AddMediatR(typeof(ChangeLightBulbStatusCommand));
            
            services.AddDbContext<HomeLightSystemContext>(options =>
             options.UseNpgsql(Configuration.GetConnectionString("ProductionConnection"),
             b => b.MigrationsAssembly("LightingSystem.API")));
            services.AddScoped<IHomeLightSystemRepository, HomeLightSystemRepository>();
            services.AddTransient<ISqlConnectionFactory>
                (x => new SqlConnectionFactory(Configuration.GetConnectionString("ProductionConnection")));

            services.AddAutoMapper(typeof(Startup), typeof(HomeLightSystemMapper));
            services.AddSignalR();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAny",
                    policy =>
                        policy.AllowCredentials()
                            .AllowAnyHeader()
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowAnyMethod()
                            .WithOrigins("http://localhost:4200", "https://localhost:44390", "https://localhost:44395",
                                "https://localhost:44318", "https://sogoodhomeautomation.firebaseapp.com" 
                                )
                            );
            });

            services.AddAuthentication(options =>
            {
                // Identity made Cookie authentication the default.
                // However, we want JWT Bearer Auth to be the default.
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(options =>
              {
                    // Configure JWT Bearer Auth to expect our security key
                    options.TokenValidationParameters =
                      new TokenValidationParameters
                      {
                          LifetimeValidator = (before, expires, token, param) =>
                          {
                              return expires > DateTime.UtcNow;
                          },
                          ValidateAudience = false,
                          ValidateIssuer = false,
                          ValidateActor = false,
                          ValidateLifetime = true,
                          IssuerSigningKey = SecurityKey
                      };

                    // We have to hook the OnMessageReceived event in order to
                    // allow the JWT authentication handler to read the access
                    // token from the query string when a WebSocket or 
                    // Server-Sent Events request comes in.
                    options.Events = new JwtBearerEvents
                  {
                      OnMessageReceived = context =>
                      {
                          var accessToken = context.Request.Query["access_token"];

                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                          if (!string.IsNullOrEmpty(accessToken) &&
                              (path.StartsWithSegments("/HomeLightSystemHub")))
                          {
                                // Read the token out of the query string
                                context.Token = accessToken;
                          }
                          else if (!string.IsNullOrEmpty(accessToken))
                          {
                              context.Token = accessToken;
                          }
                          return Task.CompletedTask;
                      }
                  };
              });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HomeAutomation.LightingSystem", Version = "v1.0.0" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAny");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
              //  app.UseHsts();
            }
            app.UseAuthentication();
            app.UseSignalR(route =>
            {
                route.MapHub<HomeLightSystemHub>("/HomeLightSystemHub");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //c.RoutePrefix = string.Empty;
            });

            // app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
