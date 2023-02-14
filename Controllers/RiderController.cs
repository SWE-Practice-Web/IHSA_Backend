using IHSA_Backend.Collections;
using IHSA_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IHSA_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RiderController : ControllerBase
    {
        private IRiderCollection _riderCollection;
        public RiderController(IRiderCollection riderCollection)
        {
            _riderCollection = riderCollection;
        }

        [HttpPost("[action]")]
        public IActionResult Create(RiderRequestModel request)
        {
            var rider = new RiderModel
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                isHeightWeightRider = request.isHeightWeightRider,
                Height = request.Height,
                Weight = request.Weight,
                ManagedBy = request.ManagedBy,
                PlaysFor = request.PlaysFor
            };
            _riderCollection.AddAsync(rider);

            return Ok();
        }
        [HttpGet]
        public IEnumerable<UserModel> ViewAll()
        {
            {
                return _riderCollection.GetAllAsync().Result;
            }
        }
    }
}
