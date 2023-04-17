using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalComputerSolutionsDb;
using GlobalComputerSolutionsDb.Core;

namespace Dahuyag_Final_Project_Software_Design.Dto
{
    public class ProjectScheduleDetail
    {
        public int ProjectScheduleId { get; set; }
        public string Description { get; set; }
        public string DateMade { get; set; }
        public int ProjectId { get; set; }
        public string ManagerName { get; set; }
        public List<TaskDetails> Tasks { get; set; } = new();

        public ProjectScheduleDetail(ProjectSchedule ps):this()
        {
            ProjectScheduleId = ps.ProjectScheduleId;
            ProjectId = ps.ProjectId;
            Description = ps.Description;
            DateMade = ps.DateMade.ToShortDateString();
            ManagerName = $"{ps.ProjectLink.ManagerLink.FirstName} {ps.ProjectLink.ManagerLink.LastName}";
            Tasks = ps.GcsTasks
                .Select(c => new TaskDetails(c))
                .ToList();
        }
        public ProjectScheduleDetail(NewProjectSchedule ps, EmployeeName manager):this()
        {
            ProjectScheduleId = ps.ProjectScheduleId;
            Description = ps.Description;
            ProjectId = ps.ProjectId;
            DateMade = ps.DateMade.ToShortDateString();
            ManagerName = manager.Name;
            Tasks = ps.Tasks;
        }

        public ProjectScheduleDetail()
        {
            DateMade = DateTime.Now.ToShortDateString();
        }
    }
}
