using IHSA_Backend.Models;
using IHSA_Backend.BLL;
using IHSA_Backend.Attributes;
using Microsoft.AspNetCore.Mvc;
using IHSA_Backend.Collections;
using IHSA_Backend.Constants;
using System.Net.NetworkInformation;

namespace IHSA_Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAuthRequestHandler _authRequestHandler;
        private readonly IAdminRequestHandler _adminRequestHandler;
        private readonly IUserRequestHandler _userRequestHandler;

        public UserController(
            IAuthRequestHandler authRequestHandler,
            IAdminRequestHandler adminRequestHandler,
            IUserRequestHandler userRequestHandler)
        {
            _authRequestHandler = authRequestHandler;
            _adminRequestHandler = adminRequestHandler;
            _userRequestHandler = userRequestHandler;
        }
        
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Authenticate(LoginRequestModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _authRequestHandler.AuthenticateAsync(request);

            return Ok(response);
        }

        [HttpGet]
        public IActionResult whoami()
        {
            var userRole = HttpContext.Items["Role"];

            if (userRole == null)
                return NoContent();

            // if ((Role)userRole == Role.Admin)
            //    return Ok(_adminRequestHandler.MapUser(
            //        HttpContext.Items["User"] as AdminModel));

            var userContext = HttpContext.Items["User"] as UserModel;

            if (userContext == null)
                return NoContent();

            var response = _authRequestHandler.MapUser(userContext);

            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userRequestHandler.GetAll();

            if (users == null || !users.Any())
                return Ok();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_userRequestHandler.IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            var user = await _userRequestHandler.Get(id);

            if (user == null || user.Equals(default(UserResponseModel)))
                return Ok();

            return Ok(user);
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> UpdateAsync(string username, UserRequestModel userRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _userRequestHandler.UpdateAsync(username, userRequest);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UserRequestModel userRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // await _userRequestHandler.UpdateAsync(username, userRequest);

            return NoContent();
        }

        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteAsync(string username)
        {
            // await _userRequestHandler.DeleteAsync(username);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            // await _userRequestHandler.Delete(id);

            return NoContent();
        }

        /*[AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAdmin(AdminRequestModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var verifedRequest = _adminRequestHandler.VerifyAdminRequest(request);

            var response = await _adminRequestHandler.Create(verifedRequest);
            
            return Ok(response);
        }*/

    }
}