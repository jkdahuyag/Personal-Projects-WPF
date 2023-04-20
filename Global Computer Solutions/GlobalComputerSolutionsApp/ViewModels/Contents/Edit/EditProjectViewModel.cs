using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Navigation;
using GlobalComputerSolutionsApp.Dto;
using GlobalComputerSolutionsApp.SubWindows.Edit;
using GlobalComputerSolutionsDb;
using Microsoft.EntityFrameworkCore;

namespace GlobalComputerSolutionsApp.ViewModels.Contents.Edit
{
    public  class EditProjectViewModel : AddProjectViewModel
    {
        public EditProjectViewModel(GcsDbContext context, ProjectCardDetails project) : base(context)
        {
            var projectToEdit = _context.Projects
                .Include(c=>c.ManagerLink)
                .ThenInclude(c=>c.RegionLink)
                .Include(c=>c.ManagerLink)
                .ThenInclude(c=>c.SkillInventories)
                .ThenInclude(c=>c.SkillLink)
                .Include(c=>c.CustomerLink)
                .ThenInclude(c=>c.RegionLink)
                .Include(c=>c.ProjectSchedules)
                .ThenInclude(c=>c.GcsTasks)
                .ThenInclude(c=>c.Assignments)
                .ThenInclude(c=>c.EmplpoyeeLink)
                .ThenInclude(c=>c.SkillInventories)
                .ThenInclude(c=>c.SkillLink)
                .Include(c=>c.ProjectSchedules)
                .ThenInclude(c=>c.GcsTasks)
                .ThenInclude(c=>c.TaskSkills)
                .ThenInclude(c=>c.SkillLink)
                .ThenInclude(c=>c.SkillInventories)
                .ThenInclude(c=>c.EmployeeLink)
                .ThenInclude(c=>c.RegionLink)
                .First(c => c.ProjectId == project.ProjectId);
           
            SavedProjectDetails = new ProjectDetails(projectToEdit);
            
            ProjectDescriptionInput = projectToEdit.Description;
            EstimatedDateStartInput = projectToEdit.EstimatedDateStart;
            EstimatedDateEndInput = projectToEdit.EstimatedDateEnd;
            ProjectEstimatedBudgetInput = projectToEdit.EstimatedBudget;
            DateStartInput = (DateTime)projectToEdit.DateStart;
            DateEndInput = (DateTime)projectToEdit.DateEnd;
            DateSignedInput = projectToEdit.DateSigned;

            CustomerSearchText = "";
            EmployeeSearchText = "";

            var employee = new EmployeeName(projectToEdit.ManagerLink);
            var customer = new CustomerName(projectToEdit.CustomerLink);

            SelectedCustomer = customer;
            SelectedEmployee = employee;

            Customers.Insert(0, customer);
            Employees.Insert(0,employee);
        }

        public override void NavigateForwards(NavigationService nav, Page currentPage)
        {
            object dummy = null;
            SaveProject(dummy);

            var nextPage = new EditProjectSchedulePage();
            nextPage.DataContext = new EditProjectScheduleViewModel(_context, SavedProjectDetails);
            nav.Navigate(nextPage);
        }
    }
}
