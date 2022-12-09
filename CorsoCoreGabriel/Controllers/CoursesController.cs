using CorsoCoreGabriel.Models.Services.Application;
using CorsoCoreGabriel.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CorsoCoreGabriel.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;

        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        public IActionResult Index()
        {
      

            List<CourseViewModel> courses = courseService.GetCourses();

            return View(courses);
        }

        public IActionResult Detail(int id)
        {

            var viewModel= courseService.GetCourse(id);

            return View(viewModel);

        }

    }
}