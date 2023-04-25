using IHSA_Backend.Models;
using IHSA_Backend.BLL;
using IHSA_Backend.Attributes;
using Microsoft.AspNetCore.Mvc;
using IHSA_Backend.Collections;
using IHSA_Backend.Constants;

namespace IHSA_Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAuthRequestHandler _authRequestHandler;
        private IUserCollection _userCollection;

        public UserController(
            IAuthRequestHandler authRequestHandler,
            IUserCollection userCollection)
        {
            _authRequestHandler = authRequestHandler;
            _userCollection = userCollection;
        }
        
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Authenticate(LoginRequestModel request, Role role)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _authRequestHandler.AuthenticateAsync(request, role);

            return Ok(response);
        }
        
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(UserRequestModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _authRequestHandler.RegisterAsync(request);

            if (response == null)
                return BadRequest();

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public IActionResult testAuth()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _authRequestHandler.parseUser(HttpContext.Items["User"] as UserModel);

            if (user == null)
                return BadRequest();

            return Ok(user);
        }
    }
}