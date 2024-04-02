using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CorsoCoreGabriel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(WebHostBuilder =>
            {
                WebHostBuilder.UseStartup<Startup>();
            })

            //Configurare per applicazioni console
            //ConfigureServices 

            .ConfigureLogging((context, builder) =>
            {
                //builder.Add PER aggiungere altri provider di log
            });
        //.UseStartup<Startup>();
    }
}
