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

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var riders = await _riderRequestHandler.GetAll();

            if (riders == null || !riders.Any())
                return Ok();

            return Ok(riders);
        }

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (_riderRequestHandler.IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            await _riderRequestHandler.Delete(id);

            return NoContent();
        }
        
        [HttpPost("[action]")]
        public async Task<IActionResult> BatchCreate(IEnumerable<RiderRequestModel> riderRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            foreach (var rider in riderRequest)
                await _riderRequestHandler.Create(rider);
            
            return Ok();
        }
    }
}
