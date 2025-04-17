using Microsoft.AspNetCore.Mvc;
using OfficeSphere.Models;

namespace OfficeSphere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : Controller
    {
        private static readonly List<Branch> _branches = new List<Branch>
        {
            new Branch { Id = 1, BranchName = "Pheonix I", Address = "123 Main St", City = "New York", State = "NY", ZipCode = "10001" },
            new Branch { Id = 2, BranchName = "Miami I", Address = "456 Elm St", City = "Los Angeles", State = "CA", ZipCode = "90001" }
        };

        // GET: api/Branch
        [HttpGet]
        public ActionResult<IEnumerable<Branch>> GetBranches()
        {
            return _branches;
        }

        // GET: api/Branch/5
        [HttpGet("{id}")]
        public ActionResult<Branch> GetBranch(int id)
        {
            var branch = _branches.Find(b => b.Id == id);
            if (branch == null)
            {
                return NotFound();
            }
            return branch;
        }

        // POST: api/Branch
        [HttpPost]
        public ActionResult<Branch> PostBranch(Branch branch)
        {
            branch.Id = _branches.Count > 0 ? _branches.Max(b => b.Id) + 1 : 1;
            _branches.Add(branch);
            return CreatedAtAction(nameof(GetBranch), new { id = branch.Id }, branch);
        }

        // PUT: api/Branch/5
        [HttpPut("{id}")]
        public IActionResult PutBranch(int id, Branch branch)
        {
            var existingBranch = _branches.Find(b => b.Id == id);
            if (existingBranch == null)
            {
                return NotFound();
            }
            existingBranch.BranchName = branch.BranchName;
            existingBranch.Address = branch.Address;
            existingBranch.City = branch.City;
            existingBranch.State = branch.State;
            existingBranch.ZipCode = branch.ZipCode;
            return NoContent();
        }

        // DELETE: api/Branch/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBranch(int id)
        {
            var branch = _branches.Find(b => b.Id == id);
            if (branch == null)
            {
                return NotFound();
            }
            _branches.Remove(branch);
            return NoContent();
        }
    }
}
