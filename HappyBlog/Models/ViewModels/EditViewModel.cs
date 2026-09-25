using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HappyBlog.Models.ViewModels
{
    public class EditViewModel
    {
        public Post post { get; set; } //

        [ValidateNever]
        public IEnumerable<SelectListItem> Categories { get; set; }//to show categories in dropdown

        [ValidateNever]
        public IFormFile? FeatureImages { get; set; } //to upload image
    }
}
