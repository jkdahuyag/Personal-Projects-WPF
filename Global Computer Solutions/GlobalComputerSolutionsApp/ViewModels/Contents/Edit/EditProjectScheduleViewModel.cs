using System.Windows.Controls;
using System.Windows.Navigation;
using GlobalComputerSolutionsApp.Dto;
using GlobalComputerSolutionsApp.SubWindows.Edit;
using GlobalComputerSolutionsDb;

namespace GlobalComputerSolutionsApp.ViewModels.Contents.Edit
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
