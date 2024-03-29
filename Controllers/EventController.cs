﻿using AutoMapper;
using IHSA_Backend.Attributes;
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
    public class EventController : ControllerBase
    {
        private readonly IEventRequestHandler _eventRequestHandler;
        public EventController(
            IEventRequestHandler eventRequestHandler)
        {
            _eventRequestHandler = eventRequestHandler;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(EventRequestModel eventRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var _event = await _eventRequestHandler.Create(eventRequest);

            if (_event == null || _event.Equals(default(EventResponseModel)))
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(_event);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var _events = await _eventRequestHandler.GetAll();

            if (_events == null || !_events.Any())
                return Ok();

            return Ok(_events);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (_eventRequestHandler.IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            var _event = await _eventRequestHandler.Get(id);
            
            if (_event == null || _event.Equals(default(EventResponseModel)))
                return Ok();

            return Ok(_event);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, EventRequestModel eventRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (_eventRequestHandler.IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            await _eventRequestHandler.Update(id, eventRequest);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (_eventRequestHandler.IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            await _eventRequestHandler.Delete(id);

            return NoContent();
        }

        [HttpPost("{id}/[action]")]
        public async Task<IActionResult> AddEventOrder(int id, EventElementOrderRequestModel eventElement)
        {
            if (_eventRequestHandler.IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            var eventOrder = await _eventRequestHandler.AddEventOrder(id, eventElement);

            if (eventOrder == null || eventOrder.Equals(default(EventElementOrderResponseModel)))
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(eventOrder);
        }

        [HttpPost("{id}/[action]")]
        public async Task<IActionResult> BatchAddEventOrder(int id, IList<EventElementOrderRequestModel> eventElements)
        {
            if (_eventRequestHandler.IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            var eventOrder = await _eventRequestHandler.BatchAddEventOrder(id, eventElements);

            if (eventOrder == null || eventOrder.Equals(default(EventElementOrderResponseModel)))
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(eventOrder);
        }

        [HttpPut("{id}/[action]")]
        public async Task<IActionResult> UpdateEventOrder(int id, EventElementOrderRequestModel eventElement)
        {
            if (_eventRequestHandler.IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            var eventOrder = await _eventRequestHandler.AddEventOrder(id, eventElement);

            if (eventOrder == null || eventOrder.Equals(default(EventElementOrderResponseModel)))
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(eventOrder);
        }

        [HttpPut("{id}/[action]")]
        public async Task<IActionResult> BatchUpdateEventOrder(int id, IList<EventElementOrderRequestModel> eventElements)
        {
            if (_eventRequestHandler.IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            var eventOrder = await _eventRequestHandler.BatchAddEventOrder(id, eventElements);

            if (eventOrder == null || eventOrder.Equals(default(EventElementOrderResponseModel)))
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(eventOrder);
        }
    }
}