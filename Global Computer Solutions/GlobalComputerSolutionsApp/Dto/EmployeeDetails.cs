using System.Collections.Generic;
using GlobalComputerSolutionsDb.Core;

namespace GlobalComputerSolutionsApp.Dto
{
    public class EmployeeDetails
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Region { get; set; }

        public List<SkillDetails> Skills { get; set; }
        public List<Assignment> Assignments { get; set; }

        public EmployeeDetails(Employee e)
        {
            EmployeeId = e.EmployeeId;
            Telephone = e.Telephone;
            Region = e.RegionLink.Name.Substring(e.RegionLink.Name.Length - 3, 2);
            Name = $"{e.FirstName} {e.MiddleName[0]}. {e.LastName}, {Region}";
            Skills = new List<SkillDetails>();
            Assignments = new List<Assignment>();
            foreach (var skillInventory in e.SkillInventories)
            {
                Skills.Add(new SkillDetails(skillInventory.SkillLink));
            }

            foreach (var assignment in e.Assignments)
            {
                Assignments.Add(assignment);
            }
        }
    }
}
