using Blogger.Data;
using Blogger.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Blogger.Controllers
{
    public class AccountController : Controller
    {
        private readonly BlogContext _context;

        public AccountController(BlogContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == username);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true // Remember me
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }
        }

        [HttpPost]
        public IActionResult SignUp(string username, string password)
        {
            var newUser = new User { Username = username, Password = BCrypt.Net.BCrypt.HashPassword(password) };
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
