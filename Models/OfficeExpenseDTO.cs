using System.Collections.Generic;

namespace OfficeSphere.Models
{
    public class DepartmentExpense
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public decimal TotalExpense { get; set; }
    }

    public class OfficeExpenseDTO
    {
        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public decimal TotalExpense { get; set; }
        public List<DepartmentExpense> DepartmentExpenses { get; set; } = new List<DepartmentExpense>();
    }
}
