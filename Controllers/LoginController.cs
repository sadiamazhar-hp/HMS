using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Diagnostics;
using System.Security.Claims;
using V._3._0.App_Data;
using V._3._0.Interfaces;
using V._3._0.Models;

namespace V._3._0.Controllers
{
    public class LoginController : Controller
    {
        /*private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }*/
        private readonly HospitalData db;
        private readonly IUser UserRepos;

        public LoginController(HospitalData _db, IUser UserRespos) {
            this.db = _db; this.UserRepos = UserRespos;
        }
        [HttpGet]
        public IActionResult Login(string returnurl = "/Modules/Patients")
        {
            return View( new UserLogin { ReturnUrl = returnurl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin LoginModel) {
            try { 
            var User = UserRepos.GetByUsernameAndPassword(
                LoginModel.Name, LoginModel.Emailid, LoginModel.Password);
            if (User == null)
            {
                    if (User == null)
                    {
                        TempData["ErrorMessage"] = "Invalid username or password.";
                        Unauthorized();
                        return RedirectToAction("Login");
                    }
                    
                    
            }
                // Automatically set the return URL based on the role
                LoginModel.SetReturnUrl(User.Roles);
                //set the Identity Cookie
                var claims = new List<Claim>
            {
                //Claims are key-value pairs that store information about the user.
                
                new Claim(ClaimTypes.NameIdentifier, User.SignupId.ToString()),
                new Claim(ClaimTypes.Name, User.Name),
                new Claim(ClaimTypes.Role,User.Roles.ToString())
            };
            //Creating a Claims Identity: Represents the user's identity with a collection of claims.
            //CookieAuthenticationDefaults.AuthenticationScheme:
            //Specifies that the authentication scheme being used is cookie-based.
            var identity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);
            //Represents the user's principal (user object) which holds the ClaimsIdentity.
            //A ClaimsPrincipal can contain multiple identities.
            var principal = new ClaimsPrincipal(identity);
            //HttpContext.SignInAsync: Signs in the user by creating an authentication cookie.
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                principal
                //, new AuthenticationProperties { IsPersistent = LoginModel.RememberMe} add this to add option remember me cookie
                //will be set in the browser even after its closed
                );
            return LocalRedirect(LoginModel.ReturnUrl);
            // Your login logic here...
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
            
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }


        public IActionResult Home()
        {
            return View(); // Will look for Views/Login/Home.cshtml
        }
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string ReturnUrl = "")
        {
            string message = "";
            var v = db.HosUser.Where(a => a.Email == login.Emailid).FirstOrDefault();
            if (v != null)
            {
                if (string.Compare(Crypto.Hash(login.Password), v.Password) == 0)
                {
                    int timeout = login.RememberMe ? 525600 : 20;
                    var ticket = new FormsAuthenticationTicket(login.Emailid, login.RememberMe, timeout);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    cookie.Expires = DateTime.Now.AddMinutes(timeout);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    message = "Invalid credential provided";
                }
            }
            else
            {
                message = "Invalid credential provided";
            }
            ViewBag.Message = message;
            return View();

        }*/

    }
}