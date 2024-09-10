﻿using DataLayer;
using Blogger.Models.Requests;
using Blogger.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ServiceLayer.Contracts;

namespace Blogger.Controllers;

public class ProfileController(IProfileService profileService) : Controller
{
    

    public IActionResult ProfileDetail()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (int.TryParse(userId, out int id))
        {
            var user = _context.Users
                .Include(u => u.Posts)
                .Include(u => u.Comments)
                .FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            ViewBag.ChangePasswordModel = new ChangePasswordResponseModel { Id = user.Id };
            return View(user);
        }

        return RedirectToAction("Login", "Account");
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
