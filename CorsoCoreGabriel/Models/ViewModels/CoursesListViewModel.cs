using CorsoCoreGabriel.Models.InputModel;
using System.Collections.Generic;

namespace CorsoCoreGabriel.Models.ViewModels
{


    public class CoursesListViewModel : IPaginationInfo
    {
        public ListViewModel<CourseViewModel> Courses { get; set; }
        public CourseInputListModel Input { get; set; }


        //Aggiunta IPaginationInfo per ridurre le propieta utilizzate se si fa il casting
        #region Implementazione IPaginationInfo

        int IPaginationInfo.CurrentPage => Input.Page;

        int IPaginationInfo.TotalResults => Courses.TotalCount;

        int IPaginationInfo.ResultsPerPage => Input.Limit;

        string IPaginationInfo.Search => Input.Search;

        string IPaginationInfo.OrderBy => Input.Orderby;

        bool IPaginationInfo.Ascending => Input.Ascending;
        #endregion
    }
}
