using Microsoft.AspNetCore.Mvc;
using OfficeSphere.Models;
using System.Xml.Serialization;

namespace OfficeSphere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : Controller
    {
        private static readonly List<Team> _teams = new List<Team>
        {
            new Team { Id = 1, Name = "Development Team", EmployeeNames = new List<string> { "John Doe", "Jane Smith" } },
            new Team { Id = 2, Name = "Marketing Team", EmployeeNames = new List<string> { "Alice Johnson", "Bob Brown" } }
        };

        // GET: api/Team
        [HttpGet]
        public ActionResult<IEnumerable<Team>> GetTeams()
        {
            return _teams;
        }

        // GET: api/Team/5
        [HttpGet("{id}")]
        public ActionResult<Team> GetTeam(int id)
        {
            var team = _teams.Find(t => t.Id == id);
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
            team.Id = _teams.Count > 0 ? _teams.Max(t => t.Id) + 1 : 1;
            _teams.Add(team);
            return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, team);
        }

        // PUT: api/Team/5
        [HttpPut("{id}")]
        public IActionResult PutTeam(int id, Team team)
        {
            var existingTeam = _teams.Find(t => t.Id == id);
            if (existingTeam == null)
            {
                return NotFound();
            }
            existingTeam.Name = team.Name;
            existingTeam.EmployeeNames = team.EmployeeNames;
            return NoContent();
        }

        // DELETE: api/Team/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            var team = _teams.Find(t => t.Id == id);
            if (team == null)
            {
                return NotFound();
            }
            _teams.Remove(team);
            return NoContent();
        }
    }
}
