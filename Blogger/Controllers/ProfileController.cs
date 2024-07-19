using Blogger.Data;
using Blogger.Entities;
using Blogger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Blogger.Controllers
{
    public class ProfileController : BaseController
    {
        public ProfileController(BlogContext context) : base(context)
        {
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

            ViewBag.ChangePasswordModel = new ChangePasswordViewModel { Id = user.Id };
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
            return View("ProfileDetail", model);
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
            return View("ProfileDetail", new { id = model.Id });
        }
    }
}
