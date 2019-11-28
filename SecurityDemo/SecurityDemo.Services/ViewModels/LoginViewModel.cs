using System.ComponentModel.DataAnnotations;

namespace SecurityDemo.Services.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Please enter user name.")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter Password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
