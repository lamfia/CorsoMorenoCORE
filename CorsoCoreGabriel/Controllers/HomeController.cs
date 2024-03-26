using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CorsoCoreGabriel.Controllers
{
    public class HomeController : Controller
    {
        //[ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]//SOLO CLIENT (browser)
        //[ResponseCache(Duration =60, Location =ResponseCacheLocation.Any)]//Default tutto il mondo ( browser, load balancer, proxy)

        [ResponseCache(CacheProfileName ="Home")]
        public IActionResult Index()
        {
            return View();
        }

    }
}