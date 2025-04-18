﻿﻿namespace OfficeSphere.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int YearsOfExperience { get; set; }
        public decimal Salary { get; set; }

        // Foreign keys
        public int DepartmentId { get; set; }
        public int TeamId { get; set; }
        public int BranchId { get; set; }
        
        // Navigation properties
        public Department Department { get; set; }
        public Team Team { get; set; }
        public Branch Branch { get; set; }

        public void CalculateSalary(string teamName = null)
        {
            // Base calculation based on role and experience
            decimal baseSalary;
            switch (Role)
            {
                case "Manager":
                    baseSalary = 5000 + (YearsOfExperience * 1000);
                    break;
                case "Developer":
                    baseSalary = 4000 + (YearsOfExperience * 800);
                    break;
                case "Designer":
                    baseSalary = 3000 + (YearsOfExperience * 700);
                    break;
                default:
                    baseSalary = 2000 + (YearsOfExperience * 500);
                    break;
            }
            
            // Apply team-based salary adjustments
            if (teamName != null)
            {
                switch (teamName)
                {
                    case "Development Team":
                        // Greater than 10000
                        Salary = Math.Max(10001, baseSalary);
                        break;
                    case "Marketing Team":
                        // Greater than 5000 and less than 8000
                        if (baseSalary <= 5000)
                            Salary = 5001;
                        else if (baseSalary >= 8000)
                            Salary = 7999;
                        else
                            Salary = baseSalary;
                        break;
                    default:
                        // Basic salary of 7000 for any other team
                        Salary = 7000;
                        break;
                }
            }
            else
            {
                Salary = baseSalary;
            }
        }
    }
}
