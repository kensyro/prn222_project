using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Franchise.MVC.WebApp.HuyLT.Filters
{
    public class AuthenticationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity == null || !context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("Login", "FranchiseStore", null);
            }
            base.OnActionExecuting(context);
        }
    }
}
