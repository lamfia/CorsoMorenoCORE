using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CorsoCoreGabriel.Models.ViewModels
{
    public class LessonViewModel
    {
        public string Title { get; set; }

        public TimeSpan Duration { get; set; }

        public static LessonViewModel FromDataRow(DataRow dataRow)
        {
            return new LessonViewModel
            {
                Title = Convert.ToString(dataRow["Title"]),
                Duration = TimeSpan.Parse(dataRow["Duration"].ToString()) ,

            };
        }
    }
}
