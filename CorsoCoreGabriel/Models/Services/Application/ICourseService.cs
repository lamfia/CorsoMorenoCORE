using CorsoCoreGabriel.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorsoCoreGabriel.Models.Services.Application
{
    public interface ICourseService
    {
        Task<ListViewModel<CourseViewModel>> GetCoursesAsync(string search, int page, string orderby, bool ascending);

        Task<CourseDetailViewModel> GetCourseAsync(int id);

        Task<List<CourseViewModel>> getBestRatingCourses();
        Task<List<CourseViewModel>> getMostRecentCourses();
    }
}
