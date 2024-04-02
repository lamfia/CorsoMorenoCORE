using CorsoCoreGabriel.Models.InputModel;
using CorsoCoreGabriel.Models.Options;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace CorsoCoreGabriel.Customization.ModelBinders
{
    public class CourseInputListModelBinder : IModelBinder
    {

        public CourseInputListModelBinder(IOptionsMonitor<CoursesOptions> optionsMonitor)
        {
            courseOptions = optionsMonitor;
        }

        public IOptionsMonitor<CoursesOptions> courseOptions { get; }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            //Get dei values
            string search = bindingContext.ValueProvider.GetValue("Search").FirstValue;
            int page = Convert.ToInt32(bindingContext.ValueProvider.GetValue("Page").FirstValue);
            string orderby = bindingContext.ValueProvider.GetValue("OrderBy").FirstValue;
            bool ascending = Convert.ToBoolean(bindingContext.ValueProvider.GetValue("Ascending").FirstValue);

            var inputModel = new CourseInputListModel(search, page, orderby, ascending, courseOptions.CurrentValue.PerPage, courseOptions.CurrentValue);

            //Binding ok!
            bindingContext.Result = ModelBindingResult.Success(inputModel);

            return Task.CompletedTask;
        }
    }
}
