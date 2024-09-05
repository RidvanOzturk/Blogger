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
    public IActionResult DeleteComment(int id)
    {
        var comment = _context.Comments.Find(id);

        if (comment == null)
        {
            return NotFound();
        }

        var username = User.Identity.Name;
        var user = _context.Users.FirstOrDefault(x => x.Username == username);

        if (comment.UserId != user.Id)
        {
            return RedirectToAction("Welcome", "Welcome");
        }

        _context.Comments.Remove(comment);
        _context.SaveChanges();

        return RedirectToAction("AllPosts", "Post");
    }


    public IActionResult Index()
    {
        return View();
    }
}
