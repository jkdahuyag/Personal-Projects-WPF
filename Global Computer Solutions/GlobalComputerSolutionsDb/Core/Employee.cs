using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalComputerSolutionsDb.Core
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }

        //links
        public int? ManagerId { get; set; }
        public Employee? ManagerLink { get; set; }
        public int RegionId { get; set; }
        public Region RegionLink { get; set; }
        
        public ICollection<SkillInventory> SkillInventories { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
