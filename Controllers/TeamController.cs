using Microsoft.AspNetCore.Mvc;
using OfficeSphere.Models;
using OfficeSphere.Services.Interfaces;

namespace OfficeSphere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        // GET: api/Team
        [HttpGet]
        public ActionResult<IEnumerable<Team>> GetTeams()
        {
            return _teamService.GetAllTeams();
        }

        // GET: api/Team/5
        [HttpGet("{id}")]
        public ActionResult<Team> GetTeam(int id)
        {
            var team = _teamService.GetTeamById(id);
            if (team == null)
            {
                return NotFound();
            }
            return team;
        }

        // POST: api/Team
        [HttpPost]
        public ActionResult<Team> PostTeam(Team team)
        {
            var addedTeam = _teamService.AddTeam(team);
            return CreatedAtAction(nameof(GetTeam), new { id = addedTeam.Id }, addedTeam);
        }

        // PUT: api/Team/5
        [HttpPut("{id}")]
        public IActionResult PutTeam(int id, Team team)
        {
            if (!_teamService.UpdateTeam(id, team))
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/Team/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            if (!_teamService.DeleteTeam(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
