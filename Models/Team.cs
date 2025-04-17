namespace OfficeSphere.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> EmployeeNames { get; set; } = new List<string>();
    }
}
