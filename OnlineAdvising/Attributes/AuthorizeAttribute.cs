using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace OnlineAdvising.Attributes
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute() : base(typeof(AuthorizeFilter))
        {
        }
    }
    
    public class AuthorizeFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var headerExists = context.HttpContext.Request.Headers.TryGetValue("userId", out var psychologyIdValue);
            if (!headerExists || psychologyIdValue[0] == "null")
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}