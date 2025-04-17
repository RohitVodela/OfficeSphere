using OfficeSphere.Models;
using OfficeSphere.Services.Interfaces;

namespace OfficeSphere.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private static readonly List<Department> _departments = new List<Department>
        {
            new Department { Id = 1, Name = "HR", Description = "Human Resources" },
            new Department { Id = 2, Name = "IT", Description = "Information Technology" }
        };

        public List<Department> GetAllDepartments()
        {
            return _departments;
        }

        public Department GetDepartmentById(int id)
        {
            return _departments.Find(d => d.Id == id);
        }

        public Department AddDepartment(Department department)
        {
            department.Id = _departments.Count > 0 ? _departments.Max(d => d.Id) + 1 : 1;
            _departments.Add(department);
            return department;
        }

        public bool UpdateDepartment(int id, Department department)
        {
            var existingDepartment = _departments.Find(d => d.Id == id);
            if (existingDepartment == null)
            {
                return false;
            }
            existingDepartment.Name = department.Name;
            existingDepartment.Description = department.Description;
            return true;
        }

        public bool DeleteDepartment(int id)
        {
            var department = _departments.Find(d => d.Id == id);
            if (department == null)
            {
                return false;
            }
            _departments.Remove(department);
            return true;
        }
    }
}
