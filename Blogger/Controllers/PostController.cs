using Blogger.Data;
using Blogger.Entities;
using Blogger.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace Blogger.Controllers
{
    public class PostController : Controller
    {
        private readonly BlogContext _context;
        public PostController(BlogContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult PostContent()
        {
            return View();
        }

        public IActionResult AllPosts()
        {
            var posts = _context.Posts
                .AsNoTracking()
                .Include(post => post.User)
                .Include(post => post.Comments)
                .ToList();
            return View(posts);
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
                var user = _context.Users
                    .FirstOrDefault(x => x.Username == username);

                if (user != null)
                {
                    post.PostedDate = DateTime.Now;
                    post.UserId = user.Id;
                    user.Posts.Add(post);
                    _context.SaveChanges();

                    return RedirectToAction("Welcome", "Welcome");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                }
            }
            return View("AllPosts", post);
        }

        [HttpPost]
        public IActionResult CreateComment(int postId, string text)
        {
            if (ModelState.IsValid)
            {
                var username = User.Identity.Name;
                var user = _context.Users
                    .FirstOrDefault(x => x.Username == username);

                if (user != null)
                {
                    var post = _context.Posts.FirstOrDefault(p => p.Id == postId);

                    if (post != null)
                    {
                        var comment = new Comment
                        {
                            Text = text,
                            CommentedDate = DateTime.Now,
                            PostId = post.Id,
                            Post = post
                        };

                        post.Comments.Add(comment);
                        _context.SaveChanges();

                        return RedirectToAction("AllPosts");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Post bulunamadı.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                }
            }
            return RedirectToAction("AllPosts");
        }
    }
}
