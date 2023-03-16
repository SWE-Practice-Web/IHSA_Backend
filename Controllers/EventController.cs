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
    public class EventController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEventCollection _eventCollection;
        public EventController(
            IMapper mapper,
            IEventCollection eventCollection)
        {
            _mapper = mapper;
            _eventCollection = eventCollection;
        }

        [HttpPost("[action]")]
        public IActionResult Create(EventRequestModel eventRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var _event = _mapper.Map<EventModel>(eventRequest);

            _eventCollection.AddAsync(_event);

            return Ok(_mapper.Map<EventRequestModel>(_event));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var _event = await _eventCollection.GetAllAsync();

            if (_event == null || !_event.Any())
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<EventRequestModel>>(_event));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            if (IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            var _event = await _eventCollection.GetByIdAsync(id);
            if (_event == null || _event.Equals(default(EventModel)))
                return NotFound();

            return Ok(_mapper.Map<EventRequestModel>(_event));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, EventRequestModel eventRequest)
        {
            if (IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingEvent = await _eventCollection.GetByIdAsync(id);
            
            if (existingEvent == null || existingEvent.Equals(default(EventModel)))
                return NotFound();

            var _event = _mapper.Map<EventModel>(eventRequest);

            _event.Id = id;

            await _eventCollection.UpdateAsync(_event);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            var existingEvent = await _eventCollection.GetByIdAsync(id);
            if (existingEvent == null || existingEvent.Equals(default(EventModel)))
                return NotFound();

            await _eventCollection.DeleteByIdAsync(id);

            return NoContent();
        }
        private bool IsInvalidId(int id)
        {
            return id < 0;
        }
    }
}
