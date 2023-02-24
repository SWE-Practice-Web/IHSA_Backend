using IHSA_Backend.Collections;
using IHSA_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IHSA_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchoolController : ControllerBase
    {
        private ISchoolCollection _schoolCollection;
        public SchoolController(ISchoolCollection schoolCollection)
        {
            _schoolCollection = schoolCollection;
        }

        [HttpPost("[action]")]
        public IActionResult Create(SchoolRequestModel schoolRequest)
        {
            var school = new SchoolModel
            {
                SchoolName = schoolRequest.SchoolName,
                Location = schoolRequest.Location,
                Latitude = schoolRequest.Latitude,
                Longitude = schoolRequest.Longitude,
                Region = schoolRequest.Region,
                Zone = schoolRequest.Zone,
                NumRiders = schoolRequest.NumRiders,
                AnchorSchool = schoolRequest.AnchorSchool,
            };

            _schoolCollection.AddAsync(school);

            return Ok();
        }
        [HttpGet]
        public IEnumerable<SchoolModel> ViewAll()
        {
            {
                return _schoolCollection.GetAllAsync().Result;
            }
        }
    }
}
