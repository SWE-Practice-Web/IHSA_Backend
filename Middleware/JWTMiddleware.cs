using IHSA_Backend.Collections;
using IHSA_Backend.Constants;
using IHSA_Backend.Helpers;
using IHSA_Backend.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IHSA_Backend.Middleware
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        // private IDictionary<int, UserModel> _contextUserCache;
        public JWTMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
            // _contextUserCache = new Dictionary<int, UserModel>();
        }
        public async Task Invoke(
            HttpContext context,
            IUserCollection userCollection,
            IAdminCollection adminCollection,
            IJWTUtils jwtUtils) 
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var userInfo = jwtUtils.ValidateToken(token);

                if (userInfo != null)
                {
                    if (userInfo.Role == Role.Admin)
                    {
                        var currentUser = await adminCollection.GetAsync(userInfo.UserId);

                        if (currentUser != null && !currentUser.Equals(default(AdminModel)))
                        {
                            context.Items["User"] = currentUser;
                            context.Items["Role"] = userInfo.Role;
                        }
                    }
                }
            }

            await _requestDelegate(context);
        }
    }
}
