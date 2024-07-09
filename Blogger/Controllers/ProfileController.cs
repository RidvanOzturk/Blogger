using BCrypt.Net;
using Blogger.Data;
using Blogger.Entities;
using Blogger.Requests;
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

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult ChangeUsername(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUsername(User model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }

                user.Username = model.Username;

                _context.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("ProfileDetail", new { id = model.Id });
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ChangePassword(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(new ChangePasswordViewModel { Id = user.Id });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }

                user.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);

                _context.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("ProfileDetail", new { id = model.Id });
            }
            return View(model);
        }
    }
}
