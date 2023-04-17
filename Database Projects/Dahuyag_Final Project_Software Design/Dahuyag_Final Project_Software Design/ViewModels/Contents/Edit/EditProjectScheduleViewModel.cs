using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using Dahuyag_Final_Project_Software_Design.Dto;
using Dahuyag_Final_Project_Software_Design.SubWindows;
using Dahuyag_Final_Project_Software_Design.SubWindows.Edit;
using GlobalComputerSolutionsDb;
using Microsoft.EntityFrameworkCore;

namespace Dahuyag_Final_Project_Software_Design.ViewModels.Contents.Edit
{
    public class EditProjectScheduleViewModel : AddProjectScheduleViewModel
    {
        public EditProjectScheduleViewModel(GcsDbContext context, ProjectDetails projectDetails) : base(context, projectDetails)
        {
            foreach (var projectSchedule in projectDetails.ProjectSchedules)
            {
                ProjectSchedules.Add(new ProjectScheduleDetail(projectSchedule, projectDetails.ManagerName));
            }
        }

        public override void NavigateForwards(NavigationService nav, Page currentPage)
        {
            object dummy = null;
            SaveProject(dummy);

            var nextPage = new EditAssignmentsPage();
            nextPage.DataContext = new EditAssignmentsViewModel(_context, _projectDetails);
            nav.Navigate(nextPage);
        }
    }
}
