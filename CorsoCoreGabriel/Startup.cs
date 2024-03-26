using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorsoCoreGabriel.Models.Options;
using CorsoCoreGabriel.Models.Services.Application;
using CorsoCoreGabriel.Models.Services.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CorsoCoreGabriel
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

           // services.AddResponseCaching(); //Response caching di middleware (attenzione, si salva sul server la prima response generata)

            services.AddMvc(options =>
            {
                var homeProfile = new Microsoft.AspNetCore.Mvc.CacheProfile();

                Configuration.Bind("ResponseCache:Home", homeProfile);

                options.CacheProfiles.Add("Home", homeProfile);
            });

            //AddTransient
            //Per servizi veloci 
            //Crea instanza e la distrugge dopo un po'
            services.AddTransient<ICachedCourseService, MemoryCacheCourseService>();



            //Interfaccia verso il DB
            services.AddTransient<ICourseService, AdoNetCourseService>();
            //services.AddTransient<ICourseService, EfCoreCourseService>();

            services.AddTransient<IDatabaseAccessor, SqliteDatabaseAccessor>();

            //AddScoped
            //Per servizi costoso da costruire (pesato)
            //Crea instanza ma non la distrugge 
            //services.AddScoped<ICourseService, CourseService>();

            //AddSingleton
            //Servizi che funzionano fuori della richiesta http e si usa SOLO UNA istanza
            //esempio: emailservice, 1 richiesta di email alla volta
            //Attenzione non è thread safe
            //services.AddSingleton<ICourseService, CourseService>();


            //services.AddScoped<MyCourseDbContext>();

            //usare questo che fa un log in comando
            //services.AddDbContext<MyCourseDbContext>();


            services.AddDbContextPool<MyCourseDbContext>(optionsBuilder =>
            {
                string configurationString = Configuration.GetConnectionString("Default");
                optionsBuilder.UseSqlite(configurationString);

            });


            //Options
            services.Configure<ConnectionStringsOptions>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<CoursesOptions>(Configuration.GetSection("CoursesOptions"));
            services.Configure<CacheOptions>(Configuration.GetSection("CacheOptions"));
            services.Configure<MemoryCacheOptions>(Configuration.GetSection("MemoryCacheOptions"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();


            //app.UseResponseCaching(); //Response Caching da middleware

            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routebuilder =>
            {
                //courses/detail/5   
                routebuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
