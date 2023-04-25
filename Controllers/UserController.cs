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
        private readonly IAdminRequestHandler _adminRequestHandler;
        private IUserCollection _userCollection;

        public UserController(
            IAuthRequestHandler authRequestHandler,
            IAdminRequestHandler adminRequestHandler,
            IUserCollection userCollection)
        {
            _authRequestHandler = authRequestHandler;
            _userCollection = userCollection;
            _adminRequestHandler = adminRequestHandler;
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

        [Authorize(Role.Default)]
        [HttpGet]
        public IActionResult whoami()
        {
            var userRole = HttpContext.Items["Role"];

            if (userRole == null)
                return NoContent();

            if ((Role)userRole == Role.Admin)
                return Ok(_adminRequestHandler.MapUser(
                    HttpContext.Items["User"] as AdminModel));

            return NoContent();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAdmin(AdminRequestModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var verifedRequest = _adminRequestHandler.VerifyAdminRequest(request);

            var response = await _adminRequestHandler.Create(verifedRequest);
            
            return Ok(response);
        }

    }
}