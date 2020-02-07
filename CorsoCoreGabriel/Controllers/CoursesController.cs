using System.Collections.Generic;
using CorsoCoreGabriel.Models.Services.Application;
using CorsoCoreGabriel.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CorsoCoreGabriel.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            var courseService = new CourseService();
            List<CourseViewModel> courses= courseService.GetCourses();
            return View(courses);
        }

        public IActionResult Detail(string id)
        {
            return View();
        }
        
    }
}