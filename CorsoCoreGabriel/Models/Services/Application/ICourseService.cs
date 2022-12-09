using CorsoCoreGabriel.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorsoCoreGabriel.Models.Services.Application
{
    public interface ICourseService
    {
        List<CourseViewModel> GetCourses();

        CourseDetailViewModel GetCourse(int id);
    }
}
