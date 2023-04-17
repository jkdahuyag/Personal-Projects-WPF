using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Dahuyag_Final_Project_Software_Design.Annotations;
using Dahuyag_Final_Project_Software_Design.Dto;
using Dahuyag_Final_Project_Software_Design.SubWindows;
using Dahuyag_Final_Project_Software_Design.SubWindows.Edit;
using Dahuyag_Final_Project_Software_Design.ViewModels.Contents.Edit;
using GlobalComputerSolutionsDb;
using GlobalComputerSolutionsDb.Core;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace Dahuyag_Final_Project_Software_Design.ViewModels.Contents
{
    public class ProjectsViewModel : INotifyPropertyChanged

    {
        /*private string _selectedNavigationDrawerItem;

        public string SelectedNavigationDrawerItem
        {
            get { return _selectedNavigationDrawerItem; }
            set
            {
                _selectedNavigationDrawerItem = value;
                OnPropertyChanged();
            }
        }
        */

        private string _projectsCardSearchText;

        private ProjectCardDetails _selecteCardDetails;

        private int _selectedProjectsInView = 1;

        public int SelectedProjectsInView
        {
            get { return _selectedProjectsInView; }
            set
            {
                _selectedProjectsInView = value; 
                OnPropertyChanged();
                FilterProjects();
            }
        }

        public ProjectCardDetails SelectedCardDetails
        {
            get { return _selecteCardDetails; }
            set
            {
                _selecteCardDetails = value; 
                OnPropertyChanged();
            }
        }


        public string ProjectsCardSearchText
        {
            get { return _projectsCardSearchText; }
            set
            {
                _projectsCardSearchText = value; 
                OnPropertyChanged();
                FilterProjects();
            }
        }

        private GcsDbContext _context;

        public ObservableCollection<int> ProjectsInView { get; set; } = new() { 1, 3, 5, 10, 20, 50 };

        public ObservableCollection<ProjectCardDetails> Projects { get; set; } 

        public ProjectsViewModel(GcsDbContext context)
        {
            _context = context;
            Projects = new ObservableCollection<ProjectCardDetails>();
        }

        public void CreateProject()
        {
            var window = new NavigationWindow();

            window.ShowsNavigationUI = false;

            var newPage = new ConfigureProjectPage();

            window.Content = newPage;

            var newProject = new AddProjectViewModel(_context);

            window.DataContext = newProject;

            window.ShowDialog();
        }

        public void EditProject()
        {
            if (SelectedCardDetails == null)
            {
                return;
            }

            var window = new NavigationWindow();

            window.ShowsNavigationUI = false;

            var newPage = new EditProjectPage();

            window.Content = newPage;

            var newProject = new EditProjectViewModel(_context,SelectedCardDetails);

            window.DataContext = newProject;

            window.ShowDialog();
        }

        public void RemoveProject()
        {
            if (SelectedCardDetails == null)
            {
                return;
            }

            var project = _context.Projects
                //.Include(c => c.ProjectSchedules)
                //.ThenInclude(c => c.GcsTasks)
                .First(c => c.ProjectId == SelectedCardDetails.ProjectId);

            var tasks = _context.GcsTasks.Include(c => c.ProjectScheduleLink)
                .ThenInclude(c => c.ProjectLink)
                .Include(c => c.Assignments)
                .Include(c => c.TaskSkills)
                .Where(c=>c.ProjectScheduleLink.ProjectId == project.ProjectId);

            var projectSchedules = _context.ProjectSchedules
                .Where(c => c.ProjectId == project.ProjectId);


            foreach (var gcsTask in tasks)
            {
                _context.RemoveRange(gcsTask.Assignments);
                _context.RemoveRange(gcsTask.TaskSkills);
                _context.Remove(gcsTask);
            }
           

            foreach (var projectSchedule in projectSchedules)
            {
                _context.Remove(projectSchedule);
            }

            //try
            //{
            //    _context.SaveChanges();
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.InnerException.Message);
            //}


            try
            {
                _context.Remove(project);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.Message);
            }

            Projects.Remove(SelectedCardDetails);
            SelectedCardDetails = null;
        }

        public void LoadProjects()
        {
            FilterProjects();
        }

        private void FilterProjects()
        {
            string search = ProjectsCardSearchText?.ToLowerInvariant() ?? string.Empty;

            var query = _context.Projects
                    .OrderBy(c=>c.EstimatedDateEnd)
                    .Include(c => c.ProjectCosts)
                    .Include(c => c.ManagerLink)
                    .Include(c => c.CustomerLink)
                    .Where(c => c.Description.ToLower().Contains(search) ||
                                 c.ProjectId.ToString().ToLower().Contains(search)
                                || c.ManagerLink.FirstName.ToLower().Contains(search)
                                || c.ManagerLink.LastName.ToLower().Contains(search)
                                || c.ManagerLink.MiddleName.ToLower().Contains(search)
                                || c.CustomerLink.FirstName.ToLower().Contains(search)
                                || c.CustomerLink.LastName.ToLower().Contains(search)
                                || c.CustomerLink.MiddleName.ToLower().Contains(search)
                                )
                ;

            var projects = query
                .Select(c => new ProjectCardDetails(c))
                .Take(SelectedProjectsInView)
                .ToList();

            Projects.Clear();

            foreach (var item in projects) Projects.Add(item);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
