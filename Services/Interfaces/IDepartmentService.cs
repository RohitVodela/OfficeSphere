using OfficeSphere.Models;

namespace OfficeSphere.Services.Interfaces
{
    public interface IDepartmentService
    {
        List<Department> GetAllDepartments();
        Department GetDepartmentById(int id);
        Department AddDepartment(Department department);
        bool UpdateDepartment(int id, Department department);
        bool DeleteDepartment(int id);
    }
}
