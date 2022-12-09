using CorsoCoreGabriel.Models.Services.Infrastructure;
using CorsoCoreGabriel.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CorsoCoreGabriel.Models.Services.Application
{
    public class AdoNetCourseService : ICourseService
    {
        private readonly IDatabaseAccessor db;

        public AdoNetCourseService(IDatabaseAccessor db)
        {
            this.db = db;
        }

        public CourseDetailViewModel GetCourse(int id)
        {
            throw new NotImplementedException();
        }

        public List<CourseViewModel> GetCourses()
        {
            string query = "SELECT * FROM Courses";

            DataSet dataSet = db.Query(query);
            throw new NotImplementedException();
        }
    }
}
