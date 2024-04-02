using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorsoCoreGabriel.Models.Services.Application;
using CorsoCoreGabriel.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorsoCoreGabriel.Controllers
{
    public class HomeController : Controller
    {
        //[ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]//SOLO CLIENT (browser)
        //[ResponseCache(Duration =60, Location =ResponseCacheLocation.Any)]//Default tutto il mondo ( browser, load balancer, proxy)


        [ResponseCache(CacheProfileName = "Home")]
        public async Task<ActionResult> Index([FromServices] ICachedCourseService courseService)
        {
            List<CourseViewModel> bestRating = await courseService.getBestRatingCourses();

            List<CourseViewModel> mostRecent = await courseService.getMostRecentCourses();

            var ViewModel = new HomeViewModel
            {
                BestRatingCourses = bestRating,
                MostRecentCourses = mostRecent
            };


            return View(ViewModel);
        }

    }
}