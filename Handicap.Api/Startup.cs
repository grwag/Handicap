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

namespace Handicap.Api
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
            services.AddLogging(logBuilder =>
            {
                logBuilder.AddSeq(Configuration.GetSection("Seq"));
            });

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://id.greshawag.com";
                    options.ApiName = "handicap_test";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("read_write", policy => policy.RequireScope("read_write"));
                options.AddPolicy("read", policy => policy.RequireScope("read"));
            });

            services.AddControllers()
                .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new DoubleConverter()));

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "Handicap Api" });
            });

            services.AddEntityFrameworkMySql().AddDbContext<HandicapContext>(opts =>
            {
                opts.UseMySql("Server=localhost;Database=handicap;User=root;Password=xyIVoRWPngF5AMFzE8DxiJt9");
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

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularDevClient",
                  builder =>
                  {
                      builder
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                  });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Handicap Api");
            });

            app.UseCors("AllowAngularDevClient");
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware(typeof(ExceptionHandling));
            app.UseHttpsRedirection();

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<HandicapContext>();
                context.Database.Migrate();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
