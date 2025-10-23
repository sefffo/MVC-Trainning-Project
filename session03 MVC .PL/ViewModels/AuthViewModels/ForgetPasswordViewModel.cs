using System.ComponentModel.DataAnnotations;

namespace session03_MVC_.PL.ViewModels.AuthViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="Email Is Invalid")]
        public string Email { get; set; }
    }
}
