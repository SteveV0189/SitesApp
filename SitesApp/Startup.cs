using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SitesApp.Models;
using Microsoft.EntityFrameworkCore;

namespace SitesApp
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
            services.AddMvc();
            services.AddEntityFrameworkInMemoryDatabase();
            services.AddDbContext<MemoryContext>(opts =>
            {
                opts.UseInMemoryDatabase("memdb");
                opts.EnableSensitiveDataLogging(true);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            PopulateDb(services.GetService<MemoryContext>());

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }

        private void PopulateDb(MemoryContext ctx)
        {
            var shiz = ctx.Sites.ToList();
            var obs = new List<Observation>()
            {
                new Observation()
                {
                    Id = 1,
                    SensorName = "Sensor0",
                    ObsValue = 42.0f,
                    ObsDateTime = DateTime.Now
                },
                new Observation()
                {
                    Id = 2,
                    SensorName = "Sensor0",
                    ObsDateTime = DateTime.Now.AddDays(1),
                    ObsValue = 43.0f
                },
                new Observation()
                {
                    Id = 3,
                    SensorName = "Sensor1",
                    ObsValue = 142.0f,
                    ObsDateTime = DateTime.Now
                },
                new Observation()
                {
                    Id = 4,
                    SensorName = "Sensor1",
                    ObsDateTime = DateTime.Now.AddDays(1),
                    ObsValue = 143.0f
                }
            };
            var sites = new List<Site>()
            {
                new Site()
                {
                    Id = 1,
                    Name = "Site0",
                },
                new Site()
                {
                    Id = 2,
                    Name = "Site1"
                }
            };
            var sensors = new List<Sensor>()
            {
                new Sensor()
                {
                    SensorName = "Sensor0",
                    SiteId = 1,
                    SensorTypeId = 0,
                },
                new Sensor()
                {
                    SensorName = "Sensor1",
                    SiteId = 2,
                    SensorTypeId = 1,
                }
            };
            ctx.Sites.AddRange(sites);
            ctx.Sensors.AddRange(sensors);
            ctx.Observations.AddRange(obs);
            ctx.SaveChanges();
        }
    }
}
