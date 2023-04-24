using IHSA_Backend.Collections;
using IHSA_Backend.Helpers;

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
                    // attach user to context on successful jwt validation
                    context.Items["User"] = userCollection.GetAsync((int)userId);
                }
            }

            await _requestDelegate(context);
        }
    }
}
