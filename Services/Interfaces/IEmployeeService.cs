using Microsoft.AspNetCore.Mvc;
using OfficeSphere.Models;

namespace OfficeSphere.Services.Interfaces
{
    public interface IEmployeeService
    {
        List<Employee> GetAllEmployees();
        List<Employee> GetSortedUniqueEmployees();
        Employee GetEmployeeById(int id);
        Employee AddEmployee(Employee employee);
        bool UpdateEmployee(int id, Employee employee);
        bool DeleteEmployee(int id);
        decimal CalculateEmployeeSalary(Employee employee);
        bool UpdateEmployeeSalary(Employee employee);
        List<Employee> SearchEmployeesByName(string name);
        List<Employee> GetEmployeesByRole(string role);
        List<Employee> GetEmployeesByDepartment(string department);
    }
}
