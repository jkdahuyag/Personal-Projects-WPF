using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalComputerSolutionsDb.Core;

namespace Dahuyag_Final_Project_Software_Design.Dto
{
    public class ProjectDetails
    {
        public int ProjectId { get; set; }
        public string Description { get; set; }
        public DateTime DateSigned { get; set; }
        public DateTime EstimatedDateStart { get; set; }
        public DateTime EstimatedDateEnd { get; set; }
        public float EstimatedBudget { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public float? TotalCost { get; set; }

        public int ManagerId { get; set; }
        public int CustomerId { get; set; }

        public List<NewProjectSchedule> ProjectSchedules { get; set; } = new();

        public EmployeeName ManagerName { get; set; }
        public CustomerName CustomerName { get; set; }

        public ProjectDetails()
        {
            
        }

        public ProjectDetails(Project p)
        {
            ProjectId = p.ProjectId;
            Description = p.Description;
            DateSigned = p.DateSigned;
            EstimatedDateStart = p.EstimatedDateStart;
            EstimatedDateEnd = p.EstimatedDateEnd;
            EstimatedBudget = p.EstimatedBudget;
            DateStart = p.DateStart;
            DateEnd = p.DateEnd;

            foreach (var projectSchedule in p.ProjectSchedules)
            {
                ProjectSchedules.Add(new NewProjectSchedule(projectSchedule));    
            }
        }
    }
}
