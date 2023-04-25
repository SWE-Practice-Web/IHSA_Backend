using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using IHSA_Backend.Models;
using IHSA_Backend.Constants;

namespace IHSA_Backend.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly Role _role;
        public AuthorizeAttribute()
        {
            _role = Role.Admin;
        }
        public AuthorizeAttribute(Role role)
        {
            _role = role;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata
                .OfType<AllowAnonymousAttribute>().Any();

            if (allowAnonymous)
                return;

            var user = context.HttpContext.Items["User"] as AuthUserBaseModel;
            if (user != null)
            {
                if ((int)user.Role > (int)_role)
                {
                    context.Result = new JsonResult(
                        new { message = "Forbidden" })
                    { StatusCode = StatusCodes.Status403Forbidden };
                }
            }
            else
            {
                context.Result = new JsonResult(
                    new { message = "Unauthorized" })
                { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }

}
