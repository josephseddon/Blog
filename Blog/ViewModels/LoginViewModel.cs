using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    //This view model abstracts the view from the application user model for logins
    public class LoginViewModel
    {
        [Required, EmailAddress, MaxLength(256), Display(Name = "Email Address")]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
