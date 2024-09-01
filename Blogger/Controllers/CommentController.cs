using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Controllers
{
    public class CommentController : Controller
    {
        private readonly BlogContext _context;
        public CommentController(BlogContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateComment(int postId, string text)
        {
            var username = User.Identity.Name;
            var user = _context.Users.
                FirstOrDefault(x => x.Username == username);

            if (user != null)
            {
                var comment = new Comment
                {
                    Text = text,
                    CommentedDate = DateTime.Now,
                    UserId = user.Id,
                    PostId = postId
                };

                _context.Comments.Add(comment);
                _context.SaveChanges();

                return RedirectToAction("AllPosts", "Post");
            }

            return RedirectToAction("Login", "Account"); 
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
}
