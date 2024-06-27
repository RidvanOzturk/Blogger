using Microsoft.AspNetCore.Mvc;

namespace Blogger.Controllers
{
    public class PostController : Controller
    {
        public IActionResult PostContent()
        {
            return View();
        }
    }
}
