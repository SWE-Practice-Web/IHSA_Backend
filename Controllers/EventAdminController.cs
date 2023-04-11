using AutoMapper;
using IHSA_Backend.BLL;
using IHSA_Backend.Collections;
using IHSA_Backend.Constants;
using IHSA_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IHSA_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventAdminController : ControllerBase
    {
        private readonly IEventAdminRequestHandler _EventAdminRequestHandler;
        public EventAdminController(
            IEventAdminRequestHandler EventAdminRequestHandler)
        {
            _EventAdminRequestHandler = EventAdminRequestHandler;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(EventAdminRequestModel EventAdminRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var EventAdmin = await _EventAdminRequestHandler.Create(EventAdminRequest);

            if (EventAdmin == null || EventAdmin.Equals(default(EventAdminResponseModel)))
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(EventAdmin);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var EventAdmin = await _EventAdminRequestHandler.GetAll();

            if (EventAdmin == null || !EventAdmin.Any())
                return Ok();

            return Ok(EventAdmin);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (_EventAdminRequestHandler.IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            var EventAdmin = await _EventAdminRequestHandler.Get(id);
            
            if (EventAdmin == null || EventAdmin.Equals(default(EventAdminResponseModel)))
                return NotFound();

            return Ok(EventAdmin);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, EventAdminRequestModel EventAdminRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_EventAdminRequestHandler.IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            await _EventAdminRequestHandler.Update(id, EventAdminRequest);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (_EventAdminRequestHandler.IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            await _EventAdminRequestHandler.Delete(id);

            return NoContent();
        }
    }
}
