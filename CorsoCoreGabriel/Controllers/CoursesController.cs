using CorsoCoreGabriel.Models.Services.Application;
using CorsoCoreGabriel.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorsoCoreGabriel.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICachedCourseService courseService;

        public CoursesController(ICachedCourseService courseService)
        {
            this.courseService = courseService;
        }

        public async Task<IActionResult> Index(string search, int page, string orderby, bool ascending)
        {

            List<CourseViewModel> courses = await courseService.GetCoursesAsync(search);

            return View(courses);
        }

        public async Task<IActionResult> Detail(int id)
        {

            CourseDetailViewModel viewModel = await courseService.GetCourseAsync(id);

            return View(viewModel);

        }

    }
}