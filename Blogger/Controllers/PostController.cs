using Blogger.Data;
using Blogger.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            var posts = _context.Posts.Include(p => p.Comments).ToList();
            return View(posts);
        }

        [HttpPost]
        public IActionResult Create(Post post)
        {
            // var data = new Blogger.Entities.Post();

            //var user = _context.Users.First(x => x.Username == );
            if (ModelState.IsValid)
            {
                post.PostedDate= DateTime.Now;
                _context.Add(post);
                _context.SaveChanges();
                return RedirectToAction("Welcome","Welcome");
            }
            return View(post);
        }
    }
}
