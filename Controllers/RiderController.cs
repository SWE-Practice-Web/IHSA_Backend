using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IRiderCollection _riderCollection;
        public RiderController(
            IMapper mapper,
            IRiderCollection riderCollection)
        {
            _mapper = mapper;
            _riderCollection = riderCollection;
        }

        [HttpPost("[action]")]
        public IActionResult Create(RiderRequestModel riderRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var rider = _mapper.Map<RiderModel>(riderRequest);

            _riderCollection.AddAsync(rider);

            return Ok(_mapper.Map<RiderRequestModel>(rider));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var riders = await _riderCollection.GetAllAsync();

            if (riders == null || !riders.Any())
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<RiderRequestModel>>(riders));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            if (IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            var rider = await _riderCollection.GetAsync(id);
            if (rider == null || rider.Equals(default(RiderModel)))
                return NotFound();

            return Ok(_mapper.Map<RiderRequestModel>(rider));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, RiderRequestModel riderRequest)
        {
            if (IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingRider = await _riderCollection.GetAsync(id);

            if (existingRider == null || existingRider.Equals(default(RiderModel)))
                return NotFound();

            var rider = _mapper.Map<RiderModel>(riderRequest);

            rider.Id = id;

            await _riderCollection.UpdateAsync(rider);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            var existingRider = await _riderCollection.GetAsync(id);
            if (existingRider == null || existingRider.Equals(default(RiderModel)))
                return NotFound();

            await _riderCollection.DeleteAsync(id);

            return NoContent();
        }
        private bool IsInvalidId(int id)
        {
            return id < 0;
        }
    }
}
