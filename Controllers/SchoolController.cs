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
    public class SchoolController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISchoolCollection _schoolCollection;
        public SchoolController(
            IMapper mapper,
            ISchoolCollection schoolCollection)
        {
            _mapper = mapper;
            _schoolCollection = schoolCollection;
        }

        [HttpPost("[action]")]
        public IActionResult Create(SchoolRequestModel schoolRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var school = _mapper.Map<SchoolModel>(schoolRequest);

            _schoolCollection.AddAsync(school);

            return Ok(_mapper.Map<SchoolRequestModel>(school));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var schools = await _schoolCollection.GetAllAsync();

            if (schools == null || !schools.Any())
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<SchoolRequestModel>>(schools));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            if (IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            var school = await _schoolCollection.GetAsync(id);
            if (school == null || school.Equals(default(SchoolModel)))
                return NotFound();

            return Ok(_mapper.Map<SchoolRequestModel>(school));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, SchoolRequestModel schoolRequest)
        {
            if (IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingSchool = await _schoolCollection.GetAsync(id);
            
            if (existingSchool == null || existingSchool.Equals(default(SchoolModel)))
                return NotFound();

            var school = _mapper.Map<SchoolModel>(schoolRequest);
            school.Id = id;

            await _schoolCollection.UpdateAsync(school);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (IsInvalidId(id))
                return BadRequest(Constant.InvalidId);

            var existingSchool = await _schoolCollection.GetAsync(id);
            if (existingSchool == null || existingSchool.Equals(default(SchoolModel)))
                return NotFound();

            await _schoolCollection.DeleteAsync(id);

            return NoContent();
        }
        private bool IsInvalidId(int id)
        {
            return id < 0;
        }
    }
}
