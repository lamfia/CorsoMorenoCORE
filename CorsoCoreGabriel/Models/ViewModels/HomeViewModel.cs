﻿using System.Collections.Generic;

namespace CorsoCoreGabriel.Models.ViewModels
{
    public class HomeViewModel
    {

        public List<CourseViewModel> BestRatingCourses { get; set; }
        public List<CourseViewModel> MostRecentCourses { get; set; }


    }
}
