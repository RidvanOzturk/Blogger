using DataLayer;
using Blogger.Models.Requests;
using Blogger.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ServiceLayer.Contracts;
using ServiceLayer.DTOs;

namespace Blogger.Controllers;


public class ProfileController(IProfileService profileService) : Controller
{


    public async Task<IActionResult> ProfileDetail(int id)
    {
        var userResult = await profileService.ProfileDetailAsync(id);
        if (!userResult.Success)
        {
            return NotFound();
        }

        // Giriş yapan kullanıcının ID'sini al
        var loggedInUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        bool isSelf = loggedInUserId == id;

        var viewModel = new UserProfileResponseModel
        {
            User = userResult.User,
            ChangePasswordModel = isSelf ? new ChangePasswordRequestModel { Id = userResult.User.Id } : null
        };

        return View(viewModel);
    }




    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequestModel model)
    {
        // Model valid ise
        if (ModelState.IsValid)
        {
            // DTO oluştur ve service ile şifreyi değiştir
            var changePasswordRequestDTO = new ChangePasswordRequestDTO
            {
                Id = model.Id,
                NewPassword = model.NewPassword,
                ConfirmPassword = model.ConfirmPassword
            };

            await profileService.ChangePasswordAsync(changePasswordRequestDTO);

            TempData["SuccessMessage"] = "Password changed successfully.";
            return RedirectToAction("ProfileDetail");
        }

        // Eğer model invalid ise, kullanıcıyı tekrar yükleyip formu geri göster
        var user = await profileService.GetUserByIdAsync(model.Id);
        if (user == null)
        {
            return NotFound();
        }

        // ViewModel tekrar oluştur
        var viewModel = new UserProfileResponseModel
        {
            User = user,
            ChangePasswordModel = new ChangePasswordRequestModel { Id = user.Id }
        };

        return View("ProfileDetail", viewModel);
    }
}
