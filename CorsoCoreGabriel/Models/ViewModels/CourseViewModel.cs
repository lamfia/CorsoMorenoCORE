using CorsoCoreGabriel.Models.Enums;
using CorsoCoreGabriel.Models.ValueTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CorsoCoreGabriel.Models.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImagePath { get; set; }

        public string Author { get; set; }

        public double Rating { get; set; }

        public Money FullPrice { get; set; }

        public Money CurrentPrice { get; set; }

        public static CourseViewModel FromDataRow(DataRow row)
        {
            var courseViewModel = new CourseViewModel { 
                
                Title= Convert.ToString(row["Title"]),
                ImagePath= Convert.ToString(row["ImagePath"]),
                Author = Convert.ToString(row["Author"]),
                Rating = Convert.ToDouble(row["Rating"]),
                FullPrice= new Money(
                    Enum.Parse <Currency>(Convert.ToString(row["FullPrice_Currency"])),
                    Convert.ToDecimal(row["FullPrice_Amount"])
                    
                    ),
                CurrentPrice= new Money(
                                       
                    Enum.Parse<Currency>(Convert.ToString(row["CurrentPrice_Currency"])),
                    Convert.ToDecimal(row["CurrentPrice_Amount"])
                    ),
                Id= Convert.ToInt32(row["Id"])


            };
            return courseViewModel;
        }
    }
}
