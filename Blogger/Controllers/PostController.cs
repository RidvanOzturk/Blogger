using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using Blogger.Models.Requests;
using ServiceLayer.Implementations;
using ServiceLayer.Contracts;
using ServiceLayer.DTOs;

namespace Blogger.Controllers;
public class PostController(IPostService postService) : Controller
{


    public IActionResult PostDetail(int id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var detail = postService.PostDetailAsync(id);
        return View(detail);

    }

    [Authorize]
    public IActionResult PostContent()
    {
        return View();
    }

    public async Task<IActionResult> AllPosts()
    {
        var posts = await postService.AllPostsAsync();
        return View(posts);
    }

    [HttpPost]
    public IActionResult DeletePost(int id)
    {
        var delPost = postService.DeletePostAsync(id);
        return RedirectToAction("AllPosts");
    }



    [HttpPost]
    public async Task<IActionResult> Create(PostCreateRequest request)
    {
        var username = User.Identity.Name;
        if (username == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var postRequestDTO = new PostCreateRequestDTO
        {
            Title = request.Title,
            Content = request.Content,
            Username = username
        };

        var res = await postService.CreatePostAsync(postRequestDTO);

        return RedirectToAction("AllPosts");

    }


}
