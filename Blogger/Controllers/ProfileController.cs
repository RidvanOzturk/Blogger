using DataLayer;
using Blogger.Models.Requests;
using Blogger.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ServiceLayer.Contracts;
using Azure.Core;
using ServiceLayer.DTOs;

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

        var (success, userProfile) = await profileService.ProfileDetailAsync(userId);

        if (!success || userProfile == null)
        {
            return NotFound();
        }

        ViewBag.ChangePasswordModel = new ChangePasswordResponseModel { Id = userProfile.Id };
        return View(userProfile);
    }


    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequestModel model)
    {
        if (ModelState.IsValid)
        {
            profileService.ChangePasswordAsync(ChangePasswordRequestDTO model);

            TempData["SuccessMessage"] = "Password changed successfully.";
            return RedirectToAction("ProfileDetail");
        }

        var currentUser = profileService.CurrentUserAsync(model);

        ViewBag.ChangePasswordModel = model;
        return View("ProfileDetail", currentUser);
    }

}
