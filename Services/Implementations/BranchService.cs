using OfficeSphere.Models;
using OfficeSphere.Services.Interfaces;

namespace OfficeSphere.Services.Implementations
{
    public class BranchService : IBranchService
    {
        private static readonly List<Branch> _branches = new List<Branch>
        {
            new Branch { Id = 1, BranchName = "Pheonix I", Address = "123 Main St", City = "New York", State = "NY", ZipCode = "10001" },
            new Branch { Id = 2, BranchName = "Miami I", Address = "456 Elm St", City = "Los Angeles", State = "CA", ZipCode = "90001" }
        };

        public List<Branch> GetAllBranches()
        {
            return _branches;
        }

        public Branch GetBranchById(int id)
        {
            return _branches.Find(b => b.Id == id);
        }

        public Branch AddBranch(Branch branch)
        {
            branch.Id = _branches.Count > 0 ? _branches.Max(b => b.Id) + 1 : 1;
            _branches.Add(branch);
            return branch;
        }

        public bool UpdateBranch(int id, Branch branch)
        {
            var existingBranch = _branches.Find(b => b.Id == id);
            if (existingBranch == null)
            {
                return false;
            }
            existingBranch.BranchName = branch.BranchName;
            existingBranch.Address = branch.Address;
            existingBranch.City = branch.City;
            existingBranch.State = branch.State;
            existingBranch.ZipCode = branch.ZipCode;
            return true;
        }

        public bool DeleteBranch(int id)
        {
            var branch = _branches.Find(b => b.Id == id);
            if (branch == null)
            {
                return false;
            }
            _branches.Remove(branch);
            return true;
        }
    }
}
