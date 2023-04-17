using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalComputerSolutionsDb.Core;

namespace Dahuyag_Final_Project_Software_Design.Dto
{
    public class NewProjectSchedule
    {

        public int ProjectScheduleId { get; set; }
        public string Description { get; set; }
        public DateTime DateMade { get; set; }
        public int ProjectId { get; set; }

        public List<TaskDetails> Tasks { get; set; } = new();

        public NewProjectSchedule()
        {
            
        }
        
        public NewProjectSchedule(ProjectSchedule projectSchedule):this()
        {
            ProjectScheduleId = projectSchedule.ProjectScheduleId;
            Description = projectSchedule.Description;
            DateMade = projectSchedule.DateMade;
            ProjectId = projectSchedule.ProjectId;

            foreach (var gcsTask in projectSchedule.GcsTasks)
            {
                Tasks.Add(new TaskDetails(gcsTask));
            }
        }
    }


}
