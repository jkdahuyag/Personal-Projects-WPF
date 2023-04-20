using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GlobalComputerSolutionsApp.Dto;
using GlobalComputerSolutionsDb;
using GlobalComputerSolutionsDb.Core;

namespace GlobalComputerSolutionsApp.ViewModels.Contents.Edit
{
    public class EditAssignmentsViewModel : AddAssignmentsViewModel
    {
        public EditAssignmentsViewModel(GcsDbContext context, ProjectDetails projectDetails) : base(context,
            projectDetails)
        {
	        
        }

        protected override void SaveProject()
        {
            var project = _context.Projects.First(c => c.ProjectId == _projectDetails.ProjectId);

            if (project.ManagerId != _projectDetails.ManagerId)
                project.ManagerId = _projectDetails.ManagerId;
            if (project.CustomerId != _projectDetails.CustomerId)
                project.CustomerId = _projectDetails.CustomerId;
            project.Description = _projectDetails.Description;
            project.EstimatedDateStart = _projectDetails.EstimatedDateStart;
            project.EstimatedDateEnd = _projectDetails.EstimatedDateEnd;
            project.EstimatedBudget = _projectDetails.EstimatedBudget;
            project.DateStart = _projectDetails.DateStart;
            project.DateEnd = _projectDetails.DateEnd;
            project.DateSigned = _projectDetails.DateSigned;
           

            if (project.DateStart == DateTime.MinValue) project.DateStart = null;
            if (project.DateEnd == DateTime.MinValue) project.DateStart = null;

            project.ProjectSchedules = new List<ProjectSchedule>();

            foreach (var projectSchedule in _projectDetails.ProjectSchedules)
            {
                var projectScheduleFromContext = _context.ProjectSchedules.First(c => c.ProjectScheduleId == projectSchedule.ProjectScheduleId);


                projectScheduleFromContext.Description = projectSchedule.Description;
                projectScheduleFromContext.DateMade = projectSchedule.DateMade;

                if (projectSchedule.ProjectScheduleId != projectScheduleFromContext.ProjectId)
                    projectScheduleFromContext.ProjectId = project.ProjectId;
                

                project.ProjectSchedules.Add(projectScheduleFromContext);

                projectScheduleFromContext.GcsTasks = new List<GcsTask>();

                foreach (var task in projectSchedule.Tasks)
                {
                    var taskFromContext = _context.GcsTasks.First(c => c.GcsTaskId == task.GcsTaskId);


                    taskFromContext.Description = task.Description;
                    taskFromContext.DateEnd = task.DateEnd;
                    taskFromContext.DateStart = task.DateStart;
                    taskFromContext.ProjectScheduleId = projectScheduleFromContext.ProjectScheduleId;
                    

                    projectScheduleFromContext.GcsTasks.Add(taskFromContext);

                    taskFromContext.Assignments = new List<Assignment>();
                    taskFromContext.TaskSkills = new List<TaskSkill>();

                    foreach (var assignedEmployee in task.AssignedEmployees)
                    {
                        taskFromContext.Assignments.Add(new Assignment()
                        {
                            EmployeeId = assignedEmployee.EmployeeId,
                            GcsTaskId = taskFromContext.GcsTaskId,
                        });
                    }

                    foreach (var skillsRequired in task.SkillsRequired)
                    {
                        taskFromContext.TaskSkills.Add(new TaskSkill
                        {
                            GcsTaskId = taskFromContext.GcsTaskId,
                            SkillId = skillsRequired.SkillId,
                            Quantity = skillsRequired.Quantity,
                        });
                    }
                }
            }
            try
            {
	            _context.SaveChanges();
            }
            catch (Exception e)
            {
	            MessageBox.Show(e.InnerException.Message);
            }
		}
    }
}
