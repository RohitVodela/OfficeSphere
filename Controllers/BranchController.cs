using Microsoft.AspNetCore.Mvc;
using OfficeSphere.Models;
using OfficeSphere.Services.Interfaces;

namespace OfficeSphere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : Controller
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        // GET: api/Branch
        [HttpGet]
        public ActionResult<IEnumerable<Branch>> GetBranches()
        {
            return _branchService.GetAllBranches();
        }

        // GET: api/Branch/5
        [HttpGet("{id}")]
        public ActionResult<Branch> GetBranch(int id)
        {
            var branch = _branchService.GetBranchById(id);
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
            var addedBranch = _branchService.AddBranch(branch);
            return CreatedAtAction(nameof(GetBranch), new { id = addedBranch.Id }, addedBranch);
        }

        // PUT: api/Branch/5
        [HttpPut("{id}")]
        public IActionResult PutBranch(int id, Branch branch)
        {
            if (!_branchService.UpdateBranch(id, branch))
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/Branch/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBranch(int id)
        {
            if (!_branchService.DeleteBranch(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
