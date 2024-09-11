using DataLayer;
using Blogger.Models.Requests;
using Blogger.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ServiceLayer.Contracts;

namespace Blogger.Controllers;

public class ProfileController(IProfileService profileService) : Controller
{


    public async Task<IActionResult> ProfileDetail()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var result = await profileService.ProfileDetailAsync(userId, out var userProfile);

        if (!result || userProfile == null)
        {
            return NotFound();
        }

        ViewBag.ChangePasswordModel = new ChangePasswordResponseModel { Id = userProfile.Id };
        return View(userProfile);
    }

    [HttpPost]
    public async Task<IActionResult> ChangeUsername(ChangeUserNameRequestModel request)
    {
        if (ModelState.IsValid)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (user == null)
            {
                return NotFound();
            }

            user.Username = request.Username;

            await _context.SaveChangesAsync();

            return RedirectToAction("ProfileDetail", new { id = request.Id });
        }
        return View("ProfileDetail", request);
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequestModel model)
    {
        if (ModelState.IsValid)
        {
            

            TempData["SuccessMessage"] = "Password changed successfully.";
            return RedirectToAction("ProfileDetail");
        }

        var currentUser = _context.Users
            .AsNoTracking()
            .Include(u => u.Posts)
            .Include(u => u.Comments)
            .FirstOrDefault(u => u.Id == model.Id);

        ViewBag.ChangePasswordModel = model;
        return View("ProfileDetail", currentUser);
    }

}
