using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Contracts;
using ServiceLayer.Implementations;

namespace Blogger.Controllers;

public class CommentController(ICommentService commentService) : Controller
{
    

    [HttpPost]
    public async Task<IActionResult> CreateComment(int postId, string text)
    {
        var username = User.Identity.Name;

        if (username == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var result = await commentService.CreateCommentAsync(postId, text, username);

        if (result)
        {
            return RedirectToAction("AllPosts", "Post");
        }

        
        ModelState.AddModelError("", "Something wrong.");
        return RedirectToAction("AllPosts", "Post");
    }
    [HttpPost]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var username = User.Identity.Name;

        if (username == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var result = await commentService.DeleteCommentAsync(id, username);

        if (result)
        {
            return RedirectToAction("AllPosts", "Post");
        }

        // Hata durumunda farklı bir sayfaya yönlendirebilirsiniz
        return RedirectToAction("Welcome", "Welcome");
    }


    public IActionResult Index()
    {
        return View();
    }
}
