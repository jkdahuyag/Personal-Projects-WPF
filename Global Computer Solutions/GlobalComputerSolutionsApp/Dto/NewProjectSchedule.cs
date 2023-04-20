using System;
using System.Collections.Generic;
using GlobalComputerSolutionsDb.Core;

namespace GlobalComputerSolutionsApp.Dto
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
