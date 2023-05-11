using AutoMapper;
using IHSA_Backend.BLL;
using IHSA_Backend.Collections;
using IHSA_Backend.Constants;
using IHSA_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IHSA_Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RiderController : ControllerBase
    {
        private readonly IRiderRequestHandler _riderRequestHandler;
        public RiderController(
            IRiderRequestHandler riderRequestHandler)
        {
            _riderRequestHandler = riderRequestHandler;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(RiderRequestModel riderRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var rider = await _riderRequestHandler.Create(riderRequest);

            if (rider == null || rider.Equals(default(RiderResponseModel)))
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(rider);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> BatchCreate(IList<RiderRequestModel> ridersRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var riders = await _riderRequestHandler.BatchCreate(ridersRequest);

            return Ok(riders);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var riders = await _riderRequestHandler.GetAll();

            if (riders == null || !riders.Any())
                return Ok();

            return Ok(riders);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (_riderRequestHandler.IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            var rider = await _riderRequestHandler.Get(id);
            
            if (rider == null || rider.Equals(default(RiderResponseModel)))
                return NotFound();

            return Ok(rider);
        }

        [AllowAnonymous]
        [HttpGet("riderid/{id}")]
        public IActionResult GetByRiderId(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_riderRequestHandler.IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            var rider = _riderRequestHandler.GetByRiderId(id);

            if (rider == null || rider.Equals(default(RiderResponseModel)))
                return NotFound();

            return Ok(rider);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, RiderRequestModel riderRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_riderRequestHandler.IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            await _riderRequestHandler.Update(id, riderRequest);

            return NoContent();
        }

        [HttpPut("riderid/{id}")]
        public async Task<IActionResult> UpdateByRiderIdAsync(int id, RiderRequestModel riderRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_riderRequestHandler.IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            await _riderRequestHandler.UpdateByRiderId(id, riderRequest);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (_riderRequestHandler.IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            await _riderRequestHandler.Delete(id);

            return NoContent();
        }

        [HttpDelete("riderid/{id}")]
        public async Task<IActionResult> DeleteByRiderIdAsync(int id)
        {
            if (_riderRequestHandler.IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            await _riderRequestHandler.DeleteByRiderId(id);

            return NoContent();
        }
    }
}
