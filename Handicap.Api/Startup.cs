using AutoMapper;
using Handicap.Api.Middleware;
using Handicap.Api.Serializers;
using Handicap.Application.Interfaces;
using Handicap.Application.Services;
using Handicap.Data.Infrastructure;
using Handicap.Data.Repo;
using Handicap.Mapping;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;

namespace Handicap.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services, ILogger<Startup> logger)
        {
            services.AddLogging(logBuilder =>
            {
                logBuilder.AddSeq(Configuration.GetSection("Seq"));
            });

            var authority = System.Environment.GetEnvironmentVariable("OAUTH_ISSUER");
            var apiName = System.Environment.GetEnvironmentVariable("OAUTH_API_NAME");
            if(Environment.IsDevelopment()){
                authority = "https://id.greshawag.com";
                apiName = "handicap_test";
            }

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = authority;
                    options.ApiName = apiName;
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("read_write", policy => policy.RequireScope("read_write"));
                options.AddPolicy("read", policy => policy.RequireScope("read"));
            });

            services.AddControllers()
                .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new DoubleConverter()));

            services.AddSpaStaticFiles(config =>
            {
                config.RootPath = "ClientApp/dist";
            });

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "Handicap Api" });
            });

            var connectionString = String.Empty;
            if (Environment.IsDevelopment())
            {
                connectionString = "Server=localhost;Database=handicap;User=root;Password=xyIVoRWPngF5AMFzE8DxiJt9";
            }
            else
            {
                connectionString = GetConnectionString();
            }

            services.AddEntityFrameworkMySql().AddDbContext<HandicapContext>(opts =>
            {
                opts.UseMySql(connectionString);
            });

            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IMatchDayRepository, MatchDayRepository>();
            services.AddScoped<IHandicapConfigurationRepository, HandicapConfigurationRepository>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IMatchDayService, MatchDayService>();
            services.AddScoped<IHandicapConfigurationService, HandicapConfigurationService>();
            services.AddScoped<IHandicapUpdateService, HandicapUpdateService>();
            services.AddScoped<IHandicapCalculator, HandicapCalculator>();


            services.AddAutoMapper(
                typeof(DomainToDtoMappingProfile)
                );

            var clientUrl = System.Environment.GetEnvironmentVariable("CLIENT_URL");
            if(Environment.IsDevelopment()){
                clientUrl = "https://localhost:5001";
            }

            logger.LogInformation($"clientUrl: {clientUrl}");
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularDevClient",
                  builder =>
                  {
                      builder
                        // .WithOrigins(clientUrl)
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                  });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Handicap Api");
            });


            app.UseMiddleware(typeof(ExceptionHandling));
            //app.UseHttpsRedirection();

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<HandicapContext>();
                context.Database.Migrate();
            }

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseCors("AllowAngularDevClient");
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        private string GetConnectionString()
        {
            var user = "root";
            var pw = System.Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
            var host = System.Environment.GetEnvironmentVariable("MYSQL_HOST");
            var db = System.Environment.GetEnvironmentVariable("MYSQL_DATABASE");
            var port = System.Environment.GetEnvironmentVariable("MYSQL_PORT");

            var connString = $"Server={host};Port={port};User={user};Password={pw};Database={db};Pooling=True";

            return connString;
        }
    }
}
