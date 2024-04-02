using CorsoCoreGabriel.Models.InputModel;
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



        public async Task<IActionResult> Index(CourseInputListModel model)
        {

            ListViewModel<CourseViewModel> courses = await courseService.GetCoursesAsync(model.Search, model.Page, model.Orderby, model.Ascending);

      

            var modelOutput = new CoursesListViewModel
            {
                Courses = courses,
                Input = model
            };

            return View(modelOutput);
        }



        public async Task<IActionResult> Detail(int id)
        {

            CourseDetailViewModel viewModel = await courseService.GetCourseAsync(id);

            return View(viewModel);

        }

    }
}