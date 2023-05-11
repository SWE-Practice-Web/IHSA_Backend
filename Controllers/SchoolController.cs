using AutoMapper;
using IHSA_Backend.Attributes;
using IHSA_Backend.BLL;
using IHSA_Backend.Collections;
using IHSA_Backend.Constants;
using IHSA_Backend.Helper;
using IHSA_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IHSA_Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolRequestHandler _schoolRequestHandler;
        private readonly IMapper _mapper;
        public SchoolController(
            ISchoolRequestHandler schoolRequestHandler,
            IMapper mapper)
        {
            _schoolRequestHandler = schoolRequestHandler;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(SchoolRequestModel schoolRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var school = await _schoolRequestHandler.Create(schoolRequest);

            if (school == null || school.Equals(default(SchoolResponseModel)))
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(school);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var schools = await _schoolRequestHandler.GetAll();

            if (schools == null || !schools.Any())
                return Ok();

            return Ok(schools);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_schoolRequestHandler.IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            var school = await _schoolRequestHandler.Get(id);

            if (school == null || school.Equals(default(SchoolResponseModel)))
                return NotFound();

            return Ok(school);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, SchoolRequestModel schoolRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_schoolRequestHandler.IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            await _schoolRequestHandler.Update(id, schoolRequest);

            return NoContent();
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> BatchUpdateAsync(IList<SchoolResponseModel> schoolRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            IList<int> ids = schoolRequest.Select(x => x.Id).ToList();

            var schoolRequests = _mapper.Map<IList<SchoolRequestModel>>(schoolRequest);

            await _schoolRequestHandler.BatchUpdate(ids, schoolRequests);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (_schoolRequestHandler.IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            await _schoolRequestHandler.Delete(id);

            return NoContent();
        }
    }
}
