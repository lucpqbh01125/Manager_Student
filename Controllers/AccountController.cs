using Manager_SIMS.Facades;
using Manager_SIMS.Models;
using Manager_SIMS.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Manager_SIMS.Controllers
{
  
    public class AccountController : Controller
    {
        private readonly IUserFacade _userFacade;

        public AccountController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Views/Account/Register.cshtml");

        }

        [HttpPost]
        public IActionResult Register(string fullName, string email, string password, int roleId, string address, string phoneNumber)
        {
            string result = _userFacade.RegisterUser(fullName, email, password, roleId, address, phoneNumber);

            if (result == "User registered successfully!")
            {
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.ErrorMessage = result;
                return View();
            }
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Account/Login.cshtml");

        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = _userFacade.LoginUser(email, password);

            if (user != null)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Role, user.Role?.RoleName ?? "Unknown") // Gán vai trò
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                switch (user.RoleId)
                {
                    case 1: return RedirectToAction("List", "Course");
                    case 2: return RedirectToAction("List", "Grade");
                    case 3: return RedirectToAction("ViewCourse", "Student");
                    default: return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.ErrorMessage = "Invalid email or password!";
            return View();
        }

      
    }
}
