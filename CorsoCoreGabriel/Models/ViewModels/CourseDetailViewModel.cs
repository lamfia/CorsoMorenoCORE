using CorsoCoreGabriel.Models.Entities;
using CorsoCoreGabriel.Models.Enums;
using CorsoCoreGabriel.Models.ValueTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CorsoCoreGabriel.Models.ViewModels
{
    public class CourseDetailViewModel : CourseViewModel
    {
        public string Description { get; set; }
        public List<LessonViewModel> Lessons { get; set; }

        public TimeSpan TotalCourseDuration
        {
            get => TimeSpan.FromSeconds(Lessons?.Sum(l => l.Duration.TotalSeconds) ?? 0);
        }

        public static new CourseDetailViewModel FromDataRow(DataRow row)
        {
            var courseViewModel = new CourseDetailViewModel
            {

                Title = Convert.ToString(row["Title"]),
                ImagePath = Convert.ToString(row["ImagePath"]),
                Description = Convert.ToString(row["Description"]),
                Author = Convert.ToString(row["Author"]),
                Rating = Convert.ToDouble(row["Rating"]),
                FullPrice = new Money(
                    Enum.Parse<Currency>(Convert.ToString(row["FullPrice_Currency"])),
                    Convert.ToDecimal(row["FullPrice_Amount"])

                    ),
                CurrentPrice = new Money(

                    Enum.Parse<Currency>(Convert.ToString(row["CurrentPrice_Currency"])),
                    Convert.ToDecimal(row["CurrentPrice_Amount"])
                    ),
                Id = Convert.ToInt32(row["Id"]),
                Lessons = new List<LessonViewModel>()

            };
            return courseViewModel;
        }

        public static new CourseDetailViewModel FromEntity(Course course)
        {
            var courseDetailViewModel = new CourseDetailViewModel
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Author = course.Author,
                ImagePath = course.ImagePath,
                Rating = course.Rating,
                CurrentPrice = course.CurrentPrice,
                FullPrice = course.FullPrice,
                Lessons=course.Lessons.Select(lesson=> new LessonViewModel { 
                    Title=lesson.Title,
                    Duration=lesson.Duration
                }).ToList()

            };
            return courseDetailViewModel;
        }
    }
}
