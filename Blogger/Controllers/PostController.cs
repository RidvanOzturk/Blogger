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

        public IActionResult PostDetail(int id)
        {
            var post = _context.Posts
                .Include(p => p.User)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.User)
                .FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
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
        [HttpDelete]
        public IActionResult DeletePost(int id)
        {
            var post = _context.Posts.Find(id);

            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            _context.SaveChanges();

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
                var user = _context.Users
                    .FirstOrDefault(x => x.Username == username);

                if (user != null)
                {
                    post.PostedDate = DateTime.Now;
                    post.UserId = user.Id;
                    user.Posts.Add(post);
                    _context.SaveChanges();

                    return RedirectToAction("AllPosts");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                }
            }
            return View("AllPosts", post);
        }

       

    }
}
