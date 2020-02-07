using System;
using System.Collections.Generic;
using CorsoCoreGabriel.Models.ViewModels;

namespace CorsoCoreGabriel.Models.Services.Application
{

    public class CourseService
    {
        public List<CourseViewModel> GetCourses()
        {
            var courseList = new List<CourseViewModel>();
            var rand =new Random();
            for (int i=0;i<=20;i++)
            {
                var price = Convert.ToDecimal(rand.NextDouble()*10+10);
                var course =new CourseViewModel
                {
                    Id=i,
                    Title=$"Corso{i}",
                    CurrentPrice= rand.NextDouble(),
                    FullPrice=rand.NextDouble(),
                    Author="Nome Cognome",
                    Rating=rand.NextDouble(),
                    ImagePath="~/logo.svg"


                };
                courseList.Add(course);
            }

            return courseList;
        }
    }

}