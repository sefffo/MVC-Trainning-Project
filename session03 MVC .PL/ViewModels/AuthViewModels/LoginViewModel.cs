using System.ComponentModel.DataAnnotations;

namespace session03_MVC_.PL.ViewModels.AuthViewModels
{
    public class LoginViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
