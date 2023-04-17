using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalComputerSolutionsDb.Core;

namespace Dahuyag_Final_Project_Software_Design.Dto
{
    public class TaskDetails
    {
        public int GcsTaskId { get; set; }
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int ProjectScheduleId { get; set; }
        public string AssignedEmployeesInString { get; set; }

        public List<SkillDetails> SkillsRequired { get; set; } = new();
        public List<EmployeeDetails> AssignedEmployees { get; set; } = new();

        public TaskDetails()
        {
            
        }

        public TaskDetails(GcsTask gcsTask):this()
        {
            GcsTaskId = gcsTask.GcsTaskId;
            Description = gcsTask.Description;
            DateStart = gcsTask.DateStart;
            DateEnd = gcsTask.DateEnd;
            ProjectScheduleId = gcsTask.ProjectScheduleId;

            foreach (var gcsTaskTaskSkill in gcsTask.TaskSkills)
            {
                SkillsRequired.Add(new SkillDetails(gcsTaskTaskSkill.SkillLink));
            }
            
            foreach (var assignment in gcsTask.Assignments)
            {
                AssignedEmployees.Add(new EmployeeDetails(assignment.EmplpoyeeLink));
            }
        }

    }
}
