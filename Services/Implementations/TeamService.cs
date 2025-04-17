using OfficeSphere.Models;
using OfficeSphere.Services.Interfaces;

namespace OfficeSphere.Services.Implementations
{
    public class TeamService : ITeamService
    {
        private static readonly List<Team> _teams = new List<Team>
        {
            new Team { Id = 1, Name = "Development Team", EmployeeNames = new List<string> { "John Doe", "Jane Smith" } },
            new Team { Id = 2, Name = "Marketing Team", EmployeeNames = new List<string> { "Alice Johnson", "Bob Brown" } }
        };

        public List<Team> GetAllTeams()
        {
            return _teams;
        }

        public Team GetTeamById(int id)
        {
            return _teams.Find(t => t.Id == id);
        }

        public Team AddTeam(Team team)
        {
            team.Id = _teams.Count > 0 ? _teams.Max(t => t.Id) + 1 : 1;
            _teams.Add(team);
            return team;
        }

        public bool UpdateTeam(int id, Team team)
        {
            var existingTeam = _teams.Find(t => t.Id == id);
            if (existingTeam == null)
            {
                return false;
            }
            existingTeam.Name = team.Name;
            existingTeam.EmployeeNames = team.EmployeeNames;
            return true;
        }

        public bool DeleteTeam(int id)
        {
            var team = _teams.Find(t => t.Id == id);
            if (team == null)
            {
                return false;
            }
            _teams.Remove(team);
            return true;
        }
    }
}
