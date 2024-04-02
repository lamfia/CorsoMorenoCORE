using CorsoCoreGabriel.Customization.ModelBinders;
using CorsoCoreGabriel.Models.Options;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CorsoCoreGabriel.Models.InputModel
{

    [ModelBinder(BinderType = typeof(CourseInputListModelBinder))]
    public class CourseInputListModel
    {
        public CourseInputListModel(string search, int page, string orderby, bool ascending, int limit, CoursesOptions coursesOptions)
        {
            //Sanitazione
         

            var orderOptions = coursesOptions.Order;

            if (!orderOptions.Allow.Contains(orderby))
            {
                orderby = orderOptions.By;
                ascending = orderOptions.Ascending;
            }

            search = search ?? "";
            page = Math.Max(page, 1);

            Page = page;
            Search = search;
            Orderby = orderby;
            Ascending = ascending;
            Limit = limit;
        }

        public string Search { get; }
        public int Page { get; }
        public int Limit { get; }
        public string Orderby { get; }
        public bool Ascending { get; }
    }
}
