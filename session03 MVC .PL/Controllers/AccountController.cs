using IKEA.DAL.Models.Auth;
using IKEA.DAL.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using session03_MVC_.PL.Controllers.Helpers;
using session03_MVC_.PL.ViewModels.AuthViewModels;

namespace session03_MVC_.PL.Controllers
{
    public class AccountController : Controller
    {
        //private readonly UserManager<AppUser> user;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            //this.user = user;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new AppUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var Res = userManager.CreateAsync(user, model.Password).Result;
            if (Res.Succeeded)
            {
                return RedirectToAction("Login");
            }
            else
            {
                foreach (var error in Res.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = userManager.FindByEmailAsync(model.Email).Result;
            if (user is not null)
            {
                // check password
                //hygeb el loogi n passowrd , mn el user elly la2eetha
                var isPasswordValid = userManager.CheckPasswordAsync(user, model.Password).Result;
                if (isPasswordValid)
                {                                                                           //remember me 
                    var Result = signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false).Result;
                    //to authinticate the user and give him the acces 

                    if (Result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Not Allowed to login");
                    if (Result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "User is locked out");
                    if (Result.Succeeded)
                        return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            return View(model);
        }

        public IActionResult SignOut()
        {
            signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction("Login");
        }



        public IActionResult ForgetPassword()
        {

            return View();


        }
        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            var user =await userManager.FindByEmailAsync(model.Email);

            if (ModelState.IsValid)
            {

                var token = userManager.GeneratePasswordResetTokenAsync(user);
                ////Acount/ResetPassword
                //btgeb el email mn el url 
                var passwordResetLink = Url.Action("ResetPassword","Account",new { email=user.Email  },Request.Scheme);
                if (user != null)
                {

                    var email = new Email()
                    {
                        Subject = "Reset Password",
                        To = user.Email,
                        Body = "Please click on the following link to reset your password: <a href='http://example.com/reset-password?email=" + user.Email + "'>Reset Password</a>"
                    };
                    //helper function to send email
                    EmailSettings.SendEmail(email); //elmaford da a7a 3k 5ales da msh el sa7 
                    return RedirectToAction(nameof(CheckYourInbox));

                    ////generate token 
                    //var token = userManager.GeneratePasswordResetTokenAsync(user).Result;
                    //var passwordResetLink = Url.Action("ForgetPassword", "Account",
                    //    new { email = model.Email, token = token }, Request.Scheme);
                    ////send email 
                    ////emailService.Send(user.Email, "Reset Password", passwordResetLink);
                    ////for demo purpose 
                    //ViewBag.link = passwordResetLink;
                    //return View("ForgetPasswordConfirmation");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email not found");
                    return View("ForgetPassword", model);
                }
            }
            else
            {
                return View("ForgetPassword", model);

            }


        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }
        public IActionResult ResetPassword()
        {
            return View();
        }

    }
}
