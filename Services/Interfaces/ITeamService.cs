using OfficeSphere.Models;

namespace OfficeSphere.Services.Interfaces
{
    public interface ITeamService
    {
        List<Team> GetAllTeams();
        Team GetTeamById(int id);
        Team AddTeam(Team team);
        bool UpdateTeam(int id, Team team);
        bool DeleteTeam(int id);
    }
}
