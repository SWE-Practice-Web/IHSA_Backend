using IHSA_Backend.Collections;
using IHSA_Backend.Helpers;
using IHSA_Backend.Models;

namespace IHSA_Backend.Middleware
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        public JWTMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        public async Task Invoke(HttpContext context, IUserCollection userCollection, IJWTUtils jwtUtils) 
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var userId = jwtUtils.ValidateToken(token);

                if (userId != null)
                {
                    var currentUser = await userCollection.GetAsync((int)userId);

                    if (currentUser != null && !currentUser.Equals(default(UserModel)))
                        context.Items["User"] = currentUser;
                }
            }

            await _requestDelegate(context);
        }
    }
}
