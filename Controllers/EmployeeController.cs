﻿﻿using Microsoft.AspNetCore.Mvc;
using OfficeSphere.Models;
using OfficeSphere.Services.Interfaces;

namespace OfficeSphere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        // GET: api/Employee
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            return _employeeService.GetAllEmployees();
        }

        // GET: api/Employee/SortedUnique
        [HttpGet("SortedUnique")]
        public ActionResult<IEnumerable<Employee>> GetSortedUniqueEmployees()
        {
            return _employeeService.GetSortedUniqueEmployees();
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
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
            var addedEmployee = _employeeService.AddEmployee(employee);
            return CreatedAtAction(nameof(GetEmployee), new { id = addedEmployee.Id }, addedEmployee);
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public IActionResult PutEmployee(int id, Employee employee)
        {
            if (!_employeeService.UpdateEmployee(id, employee))
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            if (!_employeeService.DeleteEmployee(id))
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Employee/CalculateSalary
        [HttpPost("CalculateSalary")]
        public ActionResult<decimal> CalculateSalary([FromBody] Employee employee)
        {
            return _employeeService.CalculateEmployeeSalary(employee);
        }

        // PUT: api/Employee/UpdateSalary
        [HttpPut("UpdateSalary")]
        public IActionResult UpdateSalary([FromBody] Employee employee)
        {
            if (!_employeeService.UpdateEmployeeSalary(employee))
            {
                return NotFound();
            }
            return NoContent();
        }

        // GET: api/Employee/Search/{name}
        [HttpGet("Search/{name}")]
        public ActionResult<IEnumerable<Employee>> SearchEmployeesByName(string name)
        {
            var employees = _employeeService.SearchEmployeesByName(name);
            if (!employees.Any())
            {
                return NotFound();
            }
            return employees;
        }

        // GET: api/Employee/Role/{role}
        [HttpGet("Role/{role}")]
        public ActionResult<IEnumerable<Employee>> GetEmployeesByRole(string role)
        {
            var employees = _employeeService.GetEmployeesByRole(role);
            if (!employees.Any())
            {
                return NotFound();
            }
            return employees;
        }

        // GET: api/Employee/Department/{department}
        [HttpGet("Department/{department}")]
        public ActionResult<IEnumerable<Employee>> GetEmployeesByDepartment(string department)
        {
            var employees = _employeeService.GetEmployeesByDepartment(department);
            if (!employees.Any())
            {
                return NotFound();
            }
            return employees;
        }
    }
}
