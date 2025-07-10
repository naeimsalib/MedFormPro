using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MedFormPro.Web.Data;

namespace MedFormPro.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ApplicationDbContext _context;

        public BaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
                var user = _context.Users.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    ViewData["FullName"] = user.FirstName + " " + user.LastName;
                }
                else
                {
                    ViewData["FullName"] = username;
                }
            }
            base.OnActionExecuting(context);
        }
    }
} 