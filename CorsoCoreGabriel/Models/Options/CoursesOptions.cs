﻿namespace CorsoCoreGabriel.Models.Options
{

    public class CoursesOptions
    {
        public int PerPage { get; set; }
        public CoursesOrderOptions Order { get; set; }
    }

    public class CoursesOrderOptions
    {
        public string By { get; set; }
        public bool Ascending { get; set; }
        public string[] Allow { get; set; }
    }


}
