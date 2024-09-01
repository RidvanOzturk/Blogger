using Blogger.Models.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Contracts;
using System.Security.Claims;

namespace Blogger.Controllers;

public class AccountController(IUserService userService) : Controller
{
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var claims = await userService.ValidateUserAsync(model.Username, model.Password);

        if (claims.Count != 0)
        {
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index", "Home");
        }
        else
        {
            // Hatalı giriş olursa buraya düşsün
            ModelState.AddModelError("", "Invalid username or password");
            return View(model);
        }
    }


    [HttpPost]
    public async Task<IActionResult> SignUp(string username, string password)
    {
        await userService.AddUserAsync(username, password);
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
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}
