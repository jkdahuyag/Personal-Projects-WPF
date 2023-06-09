﻿using System;
using GlobalComputerSolutionsDb.Core;

namespace GlobalComputerSolutionsApp.Dto
{
    public class ProjectCardDetails
    {
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public string Description { get; set; }
        public DateTime EstimatedDateEnd { get; set; }
        public string EstimatedBudget { get; set; }
        public DateTime? DateStart { get; set; }

        public ProjectCardDetails(Project project)
        {
            ProjectId = project.ProjectId;
            ProjectTitle = $"Project #{project.ProjectId}";
            Description = project.Description;
            EstimatedDateEnd = project.EstimatedDateEnd;
            EstimatedBudget = $"Php {project.EstimatedBudget:N}";
            DateStart = project.DateStart;
        }
    }
}
