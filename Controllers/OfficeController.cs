using Microsoft.AspNetCore.Mvc;
using OfficeSphere.Models;

namespace OfficeSphere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : Controller
    {
        private static readonly List<Office> _offices = new List<Office>
        {
            new Office { Id = 1, Name = "Arizona Intl.", Address = "123 Main St", City = "New York", State = "NY", ZipCode = "10001" },
            new Office { Id = 2, Name = "Florida Intl..", Address = "456 Elm St", City = "Los Angeles", State = "CA", ZipCode = "90001" }
        };

        // GET: api/Office
        [HttpGet]
        public ActionResult<IEnumerable<Office>> GetOffices()
        {
            return _offices;
        }

        // GET: api/Office/5
        [HttpGet("{id}")]
        public ActionResult<Office> GetOffice(int id)
        {
            var office = _offices.Find(o => o.Id == id);
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
            office.Id = _offices.Count > 0 ? _offices.Max(o => o.Id) + 1 : 1;
            _offices.Add(office);
            return CreatedAtAction(nameof(GetOffice), new { id = office.Id }, office);
        }

        // PUT: api/Office/5
        [HttpPut("{id}")]
        public IActionResult PutOffice(int id, Office office)
        {
            var existingOffice = _offices.Find(o => o.Id == id);
            if (existingOffice == null)
            {
                return NotFound();
            }
            existingOffice.Name = office.Name;
            existingOffice.Address = office.Address;
            existingOffice.City = office.City;
            existingOffice.State = office.State;
            existingOffice.ZipCode = office.ZipCode;
            return NoContent();
        }

        // DELETE: api/Office/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOffice(int id)
        {
            var office = _offices.Find(o => o.Id == id);
            if (office == null)
            {
                return NotFound();
            }
            _offices.Remove(office);
            return NoContent();
        }

        // GET: api/Office/city/{city}
        [HttpGet("city/{city}")]
        public ActionResult<IEnumerable<Office>> GetOfficesByCity(string city)
        {
            var offices = _offices.Where(o => o.City.Equals(city, StringComparison.OrdinalIgnoreCase)).ToList();
            
            if (offices.Count == 0)
            {
                return NotFound();
            }
            
            return offices;
        }
    }
}
