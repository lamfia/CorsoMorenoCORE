using CorsoCoreGabriel.Models.Enums;
using CorsoCoreGabriel.Models.ValueTypes;
using CorsoCoreGabriel.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorsoCoreGabriel.Models.Services.Application
{
    public class CourseService :ICourseService
    {
        public List<CourseViewModel> GetCourses()
        {

            var courseList = new List<CourseViewModel>();
            var rand = new Random();
            for (int i = 1; i < 20; i++)
            {
                var price = Convert.ToDecimal(rand.NextDouble() * 10 + 10);
                var course = new CourseViewModel
                {
                    Id = i,
                    Title = "Corso " + i,
                    CurrentPrice = new Money(Currency.EUR, price),
                    FullPrice = new Money(Currency.EUR, rand.NextDouble() > 0.5 ? price : price - 1),
                    Author = "Gabriel Guerra",
                    Rating = rand.Next(10, 50) / 10.0,
                    ImagePath = "/logo.svg"
                };
                courseList.Add(course);
            }

            return courseList;
        }

        public CourseDetailViewModel GetCourse(int id)
        {
            var rand = new Random();
            var price = Convert.ToDecimal(rand.NextDouble() * 10 + 10);
            var course = new CourseDetailViewModel
            {
                Id = id,
                Title = "Corso " + id,
                CurrentPrice = new Money(Currency.EUR, price),
                FullPrice = new Money(Currency.EUR, rand.NextDouble() > 0.5 ? price : price - 1),
                Author = "Gabriel Guerra",
                Rating = rand.Next(10, 50) / 10.0,
                ImagePath = "/Courses/1.jpg",
                Description="Lorem impsun",
                Lessons= new List<LessonViewModel>()
            };

            for (int i = 0; i < 5; i++)
            {
                var lesson = new LessonViewModel
                {
                    Title = "Lesson " + i,
                    Duration = TimeSpan.FromSeconds(rand.Next(40, 90))
                };
                course.Lessons.Add(lesson);

            }

            return course;
        }
    }
}
