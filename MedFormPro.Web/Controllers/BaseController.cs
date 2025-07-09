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
            base.OnActionExecuting(context);
            if (User.Identity?.IsAuthenticated == true)
            {
                var user = _context.Users.FirstOrDefault(u => u.Username == User.Identity.Name);
                if (user != null)
                {
                    ViewData["FullName"] = user.FirstName + " " + user.LastName;
                }
            }
        }
    }
} 