using Microsoft.AspNetCore.Mvc;
using OfficeSphere.Models;

namespace OfficeSphere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private static readonly List<Department> _departments = new List<Department>
        {
            new Department { Id = 1, Name = "HR", Description = "Human Resources" },
            new Department { Id = 2, Name = "IT", Description = "Information Technology" }
        };

        // GET: api/Department
        [HttpGet]
        public ActionResult<IEnumerable<Department>> GetDepartments()
        {
            return _departments;
        }

        // GET: api/Department/5
        [HttpGet("{id}")]
        public ActionResult<Department> GetDepartment(int id)
        {
            var department = _departments.Find(d => d.Id == id);
            if (department == null)
            {
                return NotFound();
            }
            return department;
        }

        // POST: api/Department
        [HttpPost]
        public ActionResult<Department> PostDepartment(Department department)
        {
            department.Id = _departments.Count > 0 ? _departments.Max(d => d.Id) + 1 : 1;
            _departments.Add(department);
            return CreatedAtAction(nameof(GetDepartment), new { id = department.Id }, department);
        }

        // PUT: api/Department/5
        [HttpPut("{id}")]
        public IActionResult PutDepartment(int id, Department department)
        {
            var existingDepartment = _departments.Find(d => d.Id == id);
            if (existingDepartment == null)
            {
                return NotFound();
            }
            existingDepartment.Name = department.Name;
            existingDepartment.Description = department.Description;
            return NoContent();
        }

        // DELETE: api/Department/5
        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            var department = _departments.Find(d => d.Id == id);
            if (department == null)
            {
                return NotFound();
            }
            _departments.Remove(department);
            return NoContent();
        }
    }
}
