using CorsoCoreGabriel.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CorsoCoreGabriel.Customization.ViewComponents
{
    public class PaginationBarViewComponent : ViewComponent
    {
        //public IViewComponentResult Invoke(CoursesListViewModel model)
        public IViewComponentResult Invoke(IPaginationInfo model)
        {
            //Il numero di pagina corrente
            //Il numero di risultati totali
            //il numero di risultati per pagina
            //Search, OrderBy e Ascending



            return View("Default", model);
        }
    }
}
