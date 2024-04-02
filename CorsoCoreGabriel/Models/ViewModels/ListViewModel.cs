using System.Collections.Generic;

namespace CorsoCoreGabriel.Models.ViewModels
{
    public class ListViewModel<T>
    {
        public List<T> List { get; set; }
        public int TotalCount { get; set; }

    }
}
