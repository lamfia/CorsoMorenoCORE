using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorsoCoreGabriel.Models.Services.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CorsoCoreGabriel
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //AddTransient
            //Per servizi veloci 
            //Crea instanza e la distrugge dopo un po'
            services.AddTransient<ICourseService, CourseService>();

            //AddScoped
            //Per servizi costoso da costruire (pesato)
            //Crea instanza ma non la distrugge 
            //services.AddScoped<ICourseService, CourseService>();

            //AddSingleton
            //Servizi che funzionano fuori della richiesta http e si usa SOLO UNA istanza
            //esempio: emailservice, 1 richiesta di email alla volta
            //Attenzione non è thread safe
            //services.AddSingleton<ICourseService, CourseService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routebuilder =>
            {
                //courses/detail/5   
                routebuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
