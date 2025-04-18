using OfficeSphere.Models;

namespace OfficeSphere.Services.Interfaces
{
    public interface IOfficeService
    {
        List<Office> GetAllOffices();
        Office GetOfficeById(int id);
        Office AddOffice(Office office);
        bool UpdateOffice(int id, Office office);
        bool DeleteOffice(int id);
        List<Office> GetOfficesByCity(string city);
        bool UpdateOfficeRegion(int branchId, string region);
        OfficeEcoSystem GetOfficeEcoSystem(int officeId);
        decimal GetOfficeExpense(int officeId);
        List<OfficeExpenseDTO> GetAllOfficeExpenses();
    }
}
