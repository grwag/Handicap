using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Handicap.Application.Services;
using Handicap.Data.Infrastructure;
using Handicap.Data.Repo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddEntityFrameworkMySql().AddDbContext<HandicapContext>(opts =>
            {
                opts.UseInMemoryDatabase("InMemoryDb");
            });

            services.AddScoped(typeof(IRepository<>), typeof(HandicapRepository<>));
            services.AddScoped<IMatchDayService, MatchDayService>();
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

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<HandicapContext>();
                context.Database.EnsureCreated();
                //context.Database.Migrate();
            }
        }
    }
}
