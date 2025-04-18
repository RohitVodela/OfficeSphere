namespace OfficeSphere.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        // Navigation property - collection of employees in this team
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
