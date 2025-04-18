using OfficeSphere.Models;
using OfficeSphere.Services.Interfaces;

namespace OfficeSphere.Services.Implementations
{
    public class OfficeService : IOfficeService
    {
        private readonly IBranchService _branchService;
        private readonly IEmployeeService _employeeService;

        public OfficeService(IBranchService branchService, IEmployeeService employeeService)
        {
            _branchService = branchService;
            _employeeService = employeeService;
        }

        private static readonly List<Office> _offices = new List<Office>
        {
            new Office { Id = 1, Name = "Arizona Intl.", Address = "123 Main St", City = "New York", State = "NY", ZipCode = "10001", OfficeRegion = "Northeast" },
            new Office { Id = 2, Name = "Florida Intl..", Address = "456 Elm St", City = "Los Angeles", State = "CA", ZipCode = "90001", OfficeRegion = "West" }
        };

        public List<Office> GetAllOffices()
        {
            return _offices;
        }

        public Office GetOfficeById(int id)
        {
            return _offices.Find(o => o.Id == id);
        }

        public Office AddOffice(Office office)
        {
            office.Id = _offices.Count > 0 ? _offices.Max(o => o.Id) + 1 : 1;
            _offices.Add(office);
            return office;
        }

        public bool UpdateOffice(int id, Office office)
        {
            var existingOffice = _offices.Find(o => o.Id == id);
            if (existingOffice == null)
            {
                return false;
            }
            existingOffice.Name = office.Name;
            existingOffice.Address = office.Address;
            existingOffice.City = office.City;
            existingOffice.State = office.State;
            existingOffice.ZipCode = office.ZipCode;
            existingOffice.OfficeRegion = office.OfficeRegion;
            return true;
        }

        public bool UpdateOfficeRegion(int branchId, string region)
        {
            var office = _offices.Find(o => o.Id == branchId);
            if (office == null)
            {
                return false;
            }
            office.OfficeRegion = region;
            return true;
        }

        public bool DeleteOffice(int id)
        {
            var office = _offices.Find(o => o.Id == id);
            if (office == null)
            {
                return false;
            }
            _offices.Remove(office);
            return true;
        }

        public List<Office> GetOfficesByCity(string city)
        {
            return _offices.Where(o => o.City.Equals(city, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public OfficeEcoSystem GetOfficeEcoSystem(int officeId)
        {
            var office = GetOfficeById(officeId);
            if (office == null)
            {
                return null;
            }

            // Get all branches and filter them based on city (assuming branches in the same city as the office)
            var branches = _branchService.GetAllBranches()
                .Where(b => b.City.Equals(office.City, StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Get all employees (assuming employees are distributed across all offices)
            // In a real application, we would filter based on office assignment
            var employees = _employeeService.GetAllEmployees();

            return new OfficeEcoSystem
            {
                Office = office,
                BranchDetails = branches,
                Employees = employees
            };
        }

        public decimal GetOfficeExpense(int officeId)
        {
            var officeEcosystem = GetOfficeEcoSystem(officeId);
            if (officeEcosystem == null || officeEcosystem.Employees == null)
            {
                return 0;
            }
            
            return officeEcosystem.Employees.Sum(e => e.Salary);
        }
    }
}
