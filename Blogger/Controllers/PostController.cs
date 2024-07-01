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
            // var data = new Blogger.Entities.Post();
            //   var user = _context.Users.First(x => x.Username == );
            // Console.WriteLine(user.Username);

            if (ModelState.IsValid)
            {
                //Kullanıcının kimlik bilgisiyle aldım.
                var username = User.Identity.Name;
                Console.WriteLine(username);
                
                //User'ı db'de buldum.
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
            return View("PostContent", post);
        }
    }
}
