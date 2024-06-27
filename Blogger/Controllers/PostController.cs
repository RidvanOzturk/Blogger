using Blogger.Data;
using Blogger.Entities;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult PostContent()
        {
            return View();
        }
       


        [HttpPost]
        public IActionResult Create(Post post)
        {
            // var data = new Blogger.Entities.Post();

            //  var user = _context.Users.First(x => x.Username == );
            if (ModelState.IsValid)
            {
                _context.Add(post);
                _context.SaveChanges();
                return RedirectToAction("Welcome","Welcome");
            }
            return View(post);
        }
    }
}
