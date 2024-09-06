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

        public IActionResult AllPosts()
        {
           var posts = postService.AllPostsAsync();

            return View(posts);
        }

        [HttpPost]
        public IActionResult DeletePost(int id)
        {
        var delPost = postService.DeletePostAsync(id);
            return RedirectToAction("AllPosts");
        }
       


        [HttpPost]
        public IActionResult Create(PostCreateRequest request)
        {
            var post = new Post()
            {
                Title = request.Title,
                Content = request.Content
            };

            if (ModelState.IsValid)
            {
            var username = User.Identity.Name;
            if (username == null)
            {
                return RedirectToAction("Login", "Account");
            }

        }
            return View("AllPosts", post);
        }

       

    }
