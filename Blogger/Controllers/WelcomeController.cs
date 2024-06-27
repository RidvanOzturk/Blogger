using Microsoft.AspNetCore.Mvc;

namespace Blogger.Controllers
{
    public class WelcomeController : Controller
    {
        public IActionResult Welcome()
        {
            return View();
        }
    }
}
