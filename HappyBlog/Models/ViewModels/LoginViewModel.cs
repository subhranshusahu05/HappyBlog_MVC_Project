using System.ComponentModel.DataAnnotations;

namespace HappyBlog.Models.ViewModels
{
    public class LoginViewModel
    {



        [Required(ErrorMessage = "Email is Required Field")]
        [EmailAddress(ErrorMessage = "Email Must be proper format")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is Required Field")]
        [DataType(DataType.Password)]
        public string Password { get; set; }





    }
}
