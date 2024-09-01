using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blogger.Controllers;

public class BaseController : Controller
{
    protected readonly BlogContext _context;

    public BaseController(BlogContext context)
    {
        _context = context;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        if (User.Identity.IsAuthenticated)
        {
            var username = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user != null)
            {
                ViewBag.UserId = user.Id;
            }
        }
    }
}
