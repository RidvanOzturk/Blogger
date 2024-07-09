using Blogger.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Controllers
{
    public class ProfileController : Controller
    {
        private readonly BlogContext _context;
        public ProfileController(BlogContext context)
        {
            _context = context;
        }
        
        public IActionResult ProfileDetail(int id)
        {
            var user = _context.Users
                 .Include(u => u.Posts)
                 .Include(u => u.Comments)
                 .FirstOrDefault(u => u.Id == id);

            return View(user);
        }

        [HttpPost]
        public IActionResult ChangeUrId(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
        }
        [HttpPost]
        public IActionResult ChangeUrPassword(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
