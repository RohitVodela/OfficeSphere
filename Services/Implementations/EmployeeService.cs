using OfficeSphere.Models;
using OfficeSphere.Services.Interfaces;

namespace OfficeSphere.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private static readonly List<Employee> _employees = new List<Employee>
        {
            new Employee { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Department = "IT" },
            new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Department = "HR" }
        };

        public List<Employee> GetAllEmployees()
        {
            return _employees;
        }

        public List<Employee> GetSortedUniqueEmployees()
        {
            return _employees
                .GroupBy(e => new { e.Id, e.LastName })
                .Select(g => g.First())
                .OrderBy(e => e.FirstName)
                .ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return _employees.Find(e => e.Id == id);
        }

        public Employee AddEmployee(Employee employee)
        {
            _employees.Add(employee);
            return employee;
        }

        public bool UpdateEmployee(int id, Employee employee)
        {
            var existingEmployee = _employees.Find(e => e.Id == id);
            if (existingEmployee == null)
            {
                return false;
            }
            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Email = employee.Email;
            existingEmployee.Department = employee.Department;
            return true;
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _employees.Find(e => e.Id == id);
            if (employee == null)
            {
                return false;
            }
            _employees.Remove(employee);
            return true;
        }

        public decimal CalculateEmployeeSalary(Employee employee)
        {
            employee.CalculateSalary();
            return employee.Salary;
        }

        public bool UpdateEmployeeSalary(Employee employee)
        {
            var existingEmployee = _employees.Find(e => e.Id == employee.Id);
            if (existingEmployee == null)
            {
                return false;
            }
            existingEmployee.Salary = employee.Salary;
            return true;
        }

        public List<Employee> SearchEmployeesByName(string name)
        {
            var employees = _employees.Where(e =>
                e.FirstName.ToLower().Contains(name.ToLower()) ||
                e.LastName.ToLower().Contains(name.ToLower())).ToList();
            
            foreach (var employee in employees)
            {
                employee.CalculateSalary();
            }
            return employees;
        }

        public List<Employee> GetEmployeesByRole(string role)
        {
            var employees = _employees.Where(e => e.Role.ToLower() == role.ToLower()).ToList();
            
            foreach (var employee in employees)
            {
                employee.CalculateSalary();
            }
            return employees;
        }

        public List<Employee> GetEmployeesByDepartment(string department)
        {
            var employees = _employees.Where(e => e.Department.ToLower() == department.ToLower()).ToList();
            
            foreach (var employee in employees)
            {
                employee.CalculateSalary();
            }
            return employees;
        }
    }
}
