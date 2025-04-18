using OfficeSphere.Models;
using OfficeSphere.Services.Interfaces;

namespace OfficeSphere.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ITeamService _teamService;

        public EmployeeService(ITeamService teamService)
        {
            _teamService = teamService;
        }
        private static readonly List<Employee> _employees = new List<Employee>
        {
            new Employee { 
                Id = 1, 
                FirstName = "John", 
                LastName = "Doe", 
                Email = "john.doe@example.com", 
                DepartmentId = 2,  // IT department
                TeamId = 1,
                BranchId = 1,
                Role = "Developer"
            },
            new Employee { 
                Id = 2, 
                FirstName = "Jane", 
                LastName = "Smith", 
                Email = "jane.smith@example.com", 
                DepartmentId = 1,  // HR department
                TeamId = 2,
                BranchId = 1,
                Role = "Manager"
            }
        };

        public List<Employee> GetAllEmployees()
        {
            foreach (var employee in _employees)
            {
                var team = _teamService.GetTeamById(employee.TeamId);
                employee.CalculateSalary(team?.Name);
            }
            return _employees;
        }

        public List<Employee> GetSortedUniqueEmployees()
        {
            var employees = _employees
                .GroupBy(e => new { e.Id, e.LastName })
                .Select(g => g.First())
                .OrderBy(e => e.FirstName)
                .ToList();
                
            foreach (var employee in employees)
            {
                var team = _teamService.GetTeamById(employee.TeamId);
                employee.CalculateSalary(team?.Name);
            }
            return employees;
        }

        public Employee GetEmployeeById(int id)
        {
            var employee = _employees.Find(e => e.Id == id);
            if (employee != null)
            {
                var team = _teamService.GetTeamById(employee.TeamId);
                employee.CalculateSalary(team?.Name);
            }
            return employee;
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
            existingEmployee.DepartmentId = employee.DepartmentId;
            existingEmployee.TeamId = employee.TeamId;
            existingEmployee.BranchId = employee.BranchId;
            existingEmployee.Role = employee.Role;
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
            var team = _teamService.GetTeamById(employee.TeamId);
            employee.CalculateSalary(team?.Name);
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
                var team = _teamService.GetTeamById(employee.TeamId);
                employee.CalculateSalary(team?.Name);
            }
            return employees;
        }

        public List<Employee> GetEmployeesByRole(string role)
        {
            var employees = _employees.Where(e => e.Role.ToLower() == role.ToLower()).ToList();
            
            foreach (var employee in employees)
            {
                var team = _teamService.GetTeamById(employee.TeamId);
                employee.CalculateSalary(team?.Name);
            }
            return employees;
        }

        public List<Employee> GetEmployeesByDepartment(int departmentId)
        {
            var employees = _employees.Where(e => e.DepartmentId == departmentId).ToList();
            
            foreach (var employee in employees)
            {
                var team = _teamService.GetTeamById(employee.TeamId);
                employee.CalculateSalary(team?.Name);
            }
            return employees;
        }

        public List<Employee> GetEmployeesByBranch(int branchId)
        {
            var employees = _employees.Where(e => e.BranchId == branchId).ToList();
            
            foreach (var employee in employees)
            {
                var team = _teamService.GetTeamById(employee.TeamId);
                employee.CalculateSalary(team?.Name);
            }
            return employees;
        }
    }
}
