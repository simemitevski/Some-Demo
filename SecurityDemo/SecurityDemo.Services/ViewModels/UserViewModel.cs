using System.ComponentModel.DataAnnotations;

namespace SecurityDemo.Services.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Please enter first name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter emb.")]
        public string Emb { get; set; }

        [Required(ErrorMessage = "Please enter user name.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        public string Password { get; set; }
    }
}
