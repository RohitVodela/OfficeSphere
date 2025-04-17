using OfficeSphere.Models;
using OfficeSphere.Services.Interfaces;

namespace OfficeSphere.Services.Implementations
{
    public class OfficeService : IOfficeService
    {
        private static readonly List<Office> _offices = new List<Office>
        {
            new Office { Id = 1, Name = "Arizona Intl.", Address = "123 Main St", City = "New York", State = "NY", ZipCode = "10001" },
            new Office { Id = 2, Name = "Florida Intl..", Address = "456 Elm St", City = "Los Angeles", State = "CA", ZipCode = "90001" }
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
    }
}
