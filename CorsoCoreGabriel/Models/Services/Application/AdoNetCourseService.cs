using CorsoCoreGabriel.Models.Options;
using CorsoCoreGabriel.Models.Services.Infrastructure;
using CorsoCoreGabriel.Models.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        private readonly IOptionsMonitor<CoursesOptions> coursesoptions;

        private ILogger<AdoNetCourseService> logger { get; }

        public AdoNetCourseService(ILogger<AdoNetCourseService> logger, IDatabaseAccessor db, IOptionsMonitor<CoursesOptions> coursesoptions)
        {
            this.coursesoptions = coursesoptions;
            this.logger = logger;
            this.db = db;
        }



        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {

            logger.LogInformation("Course {id} requested", id);

            FormattableString query = $"SELECT * FROM Courses where Id={id} ; SELECT * FROM Lessons where CourseId={id}";

            DataSet dataSet = await db.Query(query);

            //Course
            var dt = dataSet.Tables[0];
            if (dt.Rows.Count != 1)
            {
                throw new InvalidOperationException("not 1 row from course " + id);
            }
            var courseRow = dt.Rows[0];
            var courseDetailViewModel = CourseDetailViewModel.FromDataRow(courseRow);

            //Lessons
            var lessonDataTable = dataSet.Tables[1];
            foreach (DataRow dataRow in lessonDataTable.Rows)
            {
                LessonViewModel lessonViewModel = LessonViewModel.FromDataRow(dataRow);

                courseDetailViewModel.Lessons.Add(lessonViewModel);
            }

            return courseDetailViewModel;
        }

        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            FormattableString query = $"SELECT * FROM Courses";

            DataSet dataSet = await db.Query(query);

            var datatable = dataSet.Tables[0];

            var courseList = new List<CourseViewModel>();
            foreach (DataRow row in datatable.Rows)
            {
                CourseViewModel course = CourseViewModel.FromDataRow(row);

                courseList.Add(course);

            }

            return courseList;
        }
    }
}
