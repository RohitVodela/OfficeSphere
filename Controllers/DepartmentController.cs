using Microsoft.AspNetCore.Mvc;
using OfficeSphere.Models;
using OfficeSphere.Services.Interfaces;

namespace OfficeSphere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: api/Department
        [HttpGet]
        public ActionResult<IEnumerable<Department>> GetDepartments()
        {
            return _departmentService.GetAllDepartments();
        }

        // GET: api/Department/5
        [HttpGet("{id}")]
        public ActionResult<Department> GetDepartment(int id)
        {
            var department = _departmentService.GetDepartmentById(id);
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
            var addedDepartment = _departmentService.AddDepartment(department);
            return CreatedAtAction(nameof(GetDepartment), new { id = addedDepartment.Id }, addedDepartment);
        }

        // PUT: api/Department/5
        [HttpPut("{id}")]
        public IActionResult PutDepartment(int id, Department department)
        {
            if (!_departmentService.UpdateDepartment(id, department))
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/Department/5
        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            if (!_departmentService.DeleteDepartment(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
