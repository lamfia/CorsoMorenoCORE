using CorsoCoreGabriel.Models.InputModel;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CorsoCoreGabriel.Customization.TagHelpers
{
    public class OrderLinkTagHelper : AnchorTagHelper
    {
        public CourseInputListModel Input { get; set; }


        public string OrderBy { get; set; }

        public OrderLinkTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {



            output.TagName = "a";

            //Valori del link
            RouteValues["search"] = Input.Search;
            RouteValues["orderby"] = OrderBy;
            RouteValues["ascending"] = (Input.Orderby == OrderBy ? !Input.Ascending : Input.Ascending).ToString();

            base.Process(context, output);

            if (Input.Orderby == OrderBy)
            {
                var direction = Input.Ascending ? "up" : "down";
                output.PostContent.SetHtmlContent($" <i class=\"fas fa-caret-{direction}\"></i>");
            }



        }
    }
}
