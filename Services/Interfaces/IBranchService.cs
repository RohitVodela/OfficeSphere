using OfficeSphere.Models;

namespace OfficeSphere.Services.Interfaces
{
    public interface IBranchService
    {
        List<Branch> GetAllBranches();
        Branch GetBranchById(int id);
        Branch AddBranch(Branch branch);
        bool UpdateBranch(int id, Branch branch);
        bool DeleteBranch(int id);
    }
}
