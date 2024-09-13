using DataLayer;
using Blogger.Models.Requests;
using Blogger.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ServiceLayer.Contracts;
using ServiceLayer.DTOs;

namespace Blogger.Controllers;

public class ProfileController : Controller
{
    private readonly IProfileService profileService;

    // Dependency injection
    public ProfileController(IProfileService profileService)
    {
        this.profileService = profileService;
    }

    [HttpGet]
    public async Task<IActionResult> ProfileDetail()
    {
        // Kullanıcı ID'sini al
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        // Kullanıcıyı ID ile getir
        var user = await profileService.GetUserByIdAsync(int.Parse(userId));
        if (user == null)
        {
            return NotFound();
        }

        // ViewModel oluştur ve şifre değiştirme modeli ekle
        var viewModel = new UserProfileResponseModel
        {
            User = user,
            ChangePasswordModel = new ChangePasswordRequestModel { Id = user.Id }
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
