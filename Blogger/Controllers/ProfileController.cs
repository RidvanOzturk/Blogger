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
        //---- ----
        var (success, userProfile, user) = await profileService.ProfileDetailAsync(userId);

        if (!success || userProfile == null)
        {
            return NotFound();
        }

        ViewBag.ChangePasswordModel = new ChangePasswordResponseModel { Id = user.Id };
        return View(user);
    }


    public async Task<IActionResult> ChangePassword(ChangePasswordRequestModel model)
    {
        
            var changePasswordRequestDTO = new ChangePasswordRequestDTO
            {
                Id = model.Id,
                NewPassword = model.NewPassword,
                ConfirmPassword = model.ConfirmPassword
            };
            await profileService.ChangePasswordAsync(changePasswordRequestDTO);
            TempData["SuccessMessage"] = "Password changed successfully.";
        var currentUser = await profileService.CurrentUserAsync(model.Id);
        ViewBag.ChangePasswordModel = model;
        return View("ProfileDetail", currentUser);
    }

}
