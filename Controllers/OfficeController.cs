using Microsoft.AspNetCore.Mvc;
using OfficeSphere.Models;
using OfficeSphere.Services.Interfaces;

namespace OfficeSphere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : Controller
    {
        private readonly IOfficeService _officeService;

        public OfficeController(IOfficeService officeService)
        {
            _officeService = officeService;
        }

        // GET: api/Office
        [HttpGet]
        public ActionResult<IEnumerable<Office>> GetOffices()
        {
            return _officeService.GetAllOffices();
        }

        // GET: api/Office/5
        [HttpGet("{id}")]
        public ActionResult<Office> GetOffice(int id)
        {
            var office = _officeService.GetOfficeById(id);
            if (office == null)
            {
                return NotFound();
            }
            return office;
        }

        // POST: api/Office
        [HttpPost]
        public ActionResult<Office> PostOffice(Office office)
        {
            var addedOffice = _officeService.AddOffice(office);
            return CreatedAtAction(nameof(GetOffice), new { id = addedOffice.Id }, addedOffice);
        }

        // PUT: api/Office/5
        [HttpPut("{id}")]
        public IActionResult PutOffice(int id, Office office)
        {
            if (!_officeService.UpdateOffice(id, office))
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/Office/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOffice(int id)
        {
            if (!_officeService.DeleteOffice(id))
            {
                return NotFound();
            }
            return NoContent();
        }

        // GET: api/Office/city/{city}
        [HttpGet("city/{city}")]
        public ActionResult<IEnumerable<Office>> GetOfficesByCity(string city)
        {
            var offices = _officeService.GetOfficesByCity(city);
            
            if (!offices.Any())
            {
                return NotFound();
            }
            
            return offices;
        }

        // PATCH: api/Office/{id}/region
        [HttpPatch("{id}/region")]
        public IActionResult UpdateOfficeRegion(int id, [FromBody] string region)
        {
            if (string.IsNullOrWhiteSpace(region))
            {
                return BadRequest("Region cannot be empty");
            }

            if (!_officeService.UpdateOfficeRegion(id, region))
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
