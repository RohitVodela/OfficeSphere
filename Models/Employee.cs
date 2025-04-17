﻿﻿﻿namespace OfficeSphere.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }
        public int YearsOfExperience { get; set; }
        public decimal Salary { get; set; }

        public void CalculateSalary()
        {
            switch (Role)
            {
                case "Manager":
                    Salary = 5000 + (YearsOfExperience * 1000);
                    break;
                case "Developer":
                    Salary = 4000 + (YearsOfExperience * 800);
                    break;
                case "Designer":
                    Salary = 3000 + (YearsOfExperience * 700);
                    break;
                default:
                    Salary = 2000 + (YearsOfExperience * 500);
                    break;
            }
        }
    }
}
