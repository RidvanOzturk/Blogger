using Microsoft.AspNetCore.Mvc;

namespace Blogger.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
