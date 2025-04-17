﻿using Microsoft.AspNetCore.Mvc;
using OfficeSphere.Models;

namespace OfficeSphere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private static readonly List<Employee> _employees = new List<Employee>
        {
            new Employee { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Department = "IT" },
            new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Department = "HR" }
        };


        // GET: api/Employee
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            return _employees;
        }

        // GET: api/Employee/SortedUnique
        [HttpGet("SortedUnique")]
        public ActionResult<IEnumerable<Employee>> GetSortedUniqueEmployees()
        {
            // Get distinct employees based on Id and LastName, then sort by FirstName
            return _employees
                .GroupBy(e => new { e.Id, e.LastName })
                .Select(g => g.First())
                .OrderBy(e => e.FirstName)
                .ToList();
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            var employee = _employees.Find(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        // POST: api/Employee
        [HttpPost]
        public ActionResult<Employee> PostEmployee(Employee employee)
        {
            _employees.Add(employee);
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public IActionResult PutEmployee(int id, Employee employee)
        {
            var existingEmployee = _employees.Find(e => e.Id == id);
            if (existingEmployee == null)
            {
                return NotFound();
            }
            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Email = employee.Email;
            existingEmployee.Department = employee.Department;
            return NoContent();
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _employees.Find(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            _employees.Remove(employee);
            return NoContent();
        }

        // POST: api/Employee/CalculateSalary
        [HttpPost("CalculateSalary")]
        public ActionResult<decimal> CalculateSalary([FromBody] Employee employee)
        {
            employee.CalculateSalary();
            return employee.Salary;
        }

        // PUT: api/Employee/UpdateSalary
        [HttpPut("UpdateSalary")]
        public IActionResult UpdateSalary([FromBody] Employee employee)
        {
            var existingEmployee = _employees.Find(e => e.Id == employee.Id);
            if (existingEmployee == null)
            {
                return NotFound();
            }
            existingEmployee.Salary = employee.Salary;
            return NoContent();
        }

        // GET: api/Employee/Search/{name}
        [HttpGet("Search/{name}")]
        public ActionResult<IEnumerable<Employee>> SearchEmployeesByName(string name)
        {
            var employees = _employees.Where(e =>
                e.FirstName.ToLower().Contains(name.ToLower()) ||
                e.LastName.ToLower().Contains(name.ToLower())).ToList();
            if (!employees.Any())
            {
                return NotFound();
            }
            foreach (var employee in employees)
            {
                employee.CalculateSalary();
            }
            return employees;
        }

        // GET: api/Employee/Role/{role}
        [HttpGet("Role/{role}")]
        public ActionResult<IEnumerable<Employee>> GetEmployeesByRole(string role)
        {
            var employees = _employees.Where(e => e.Role.ToLower() == role.ToLower()).ToList();
            if (!employees.Any())
            {
                return NotFound();
            }
            foreach (var employee in employees)
            {
                employee.CalculateSalary();
            }
            return employees;
        }

        // GET: api/Employee/Department/{department}
        [HttpGet("Department/{department}")]
        public ActionResult<IEnumerable<Employee>> GetEmployeesByDepartment(string department)
        {
            var employees = _employees.Where(e => e.Department.ToLower() == department.ToLower()).ToList();
            if (!employees.Any())
            {
                return NotFound();
            }
            foreach (var employee in employees)
            {
                employee.CalculateSalary();
            }
            return employees;
        }
    }
}
