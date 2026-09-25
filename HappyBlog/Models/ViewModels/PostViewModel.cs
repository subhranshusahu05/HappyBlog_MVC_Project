using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HappyBlog.Models.ViewModels
{
    public class PostViewModel
    {
        public Post post { get; set; } //


        [ValidateNever]

        public IEnumerable<SelectListItem> Categories { get; set; }//to show categories in dropdown

        public IFormFile? FeatureImages { get; set; }
    }
}
