using System.Collections.Generic;

namespace OfficeSphere.Models
{
    public class OfficeEcoSystem
    {
        public Office Office { get; set; }
        public List<Branch> BranchDetails { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
