using Dahuyag_Final_Project_Software_Design.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dahuyag_Final_Project_Software_Design.Annotations;
using GlobalComputerSolutionsDb;
using GlobalComputerSolutionsDb.Core;
using Microsoft.EntityFrameworkCore;
using Dahuyag_Final_Project_Software_Design.SubWindows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows;

namespace Dahuyag_Final_Project_Software_Design.ViewModels
{
    public class AddAssignmentsViewModel:INotifyPropertyChanged
    {
        private int _addLimit = 0;

        private ProjectScheduleDetail _selectedSchedule;

        private string _availableEmployeeSearchText;

        protected ProjectDetails _projectDetails;

        protected readonly GcsDbContext _context;

        private EmployeeDetails _selectedAvailableEmployee;

        private TaskDetails _selectedTaskOnList;

        private Action _saveAction;

        private SkillName _selectedSkillRequirement;

        public SkillName SelectedSkillRequirement
        {
            get { return _selectedSkillRequirement; }
            set
            {
                _selectedSkillRequirement = value; 
                OnPropertyChanged();
                //if (SelectedTaskOnList != null && value != null)
                //{
                //    _addLimit = SelectedTaskOnList.SkillsRequired
                //        .FirstOrDefault(c => c.SkillId == SelectedSkillRequirement.SkillId)
                //        .Quantity;
                //}
                LoadAvailableEmployees();
            }
        }

        public TaskDetails SelectedTaskOnList
        {
            get { return _selectedTaskOnList; }
            set
            {
                _selectedTaskOnList = value; 
                OnPropertyChanged();
                SelectedSkillRequirement = null;
                LoadSkillsRequired();
                LoadAvailableEmployees();
            }
        }

        public EmployeeDetails SelectedAvailableEmployee
        {
            get { return _selectedAvailableEmployee; }
            set
            {
                _selectedAvailableEmployee = value;
                OnPropertyChanged();
            }
        }

        public string AvailableEmployeeSearchText
        {
            get { return _availableEmployeeSearchText; }
            set
            {
                _availableEmployeeSearchText = value;
                OnPropertyChanged();
            }
        }

        public ProjectScheduleDetail SelectedSchedule
        {
            get { return _selectedSchedule; }
            set
            {
                _selectedSchedule = value;
                LoadProjectScheduleDetails(value);
                OnPropertyChanged();
            }
        }

        public string UnAssignedSkills { get; set; } = "";

        public ObservableCollection<TaskDetails> TaskList { get; set; } = new();

        public ObservableCollection<SkillName> SkillsRequired { get; set; } = new();

        public ObservableCollection<ProjectScheduleDetail> ProjectSchedules { get; set; } = new();

        public ObservableCollection<EmployeeDetails> AvailableEmployees { get; set; } = new();

        public AddAssignmentsViewModel(GcsDbContext context, ProjectDetails projectDetails)
        {
            _context = context;
            _projectDetails = projectDetails;
        }

        public void LoadProjectSchedules()
        {
            foreach (var projectSchedule in _projectDetails.ProjectSchedules)
            {
                ProjectSchedules.Add(new ProjectScheduleDetail(projectSchedule, _projectDetails.ManagerName));
            }
        }

        public void LoadProjectScheduleDetails(ProjectScheduleDetail p)
        {
            if (p == null)
            {
                return;
            }
            TaskList.Clear();
            foreach (var pTask in p.Tasks)
            {
                TaskList.Add(pTask);
            }
        }

        public void LoadAvailableEmployees()
        {
            FilterAvailableEmployees();
        }

        private void FilterAvailableEmployees()
        {
            if (SelectedSkillRequirement == null)
            {
                return;
            }

            string search = AvailableEmployeeSearchText?.ToLowerInvariant() ?? string.Empty;

            var query = _context.Employees
                    .Include(c => c.RegionLink)
                    .Include(c=>c.SkillInventories)
                    .ThenInclude(c=>c.SkillLink)
                    .Where(c => (c.LastName.ToLower().Contains(search)
                    || c.FirstName.ToLower().Contains(search)
                    || c.MiddleName.ToLower().Contains(search)
                    || c.Telephone.Contains(search))
                    && c.RegionLink.Name
                        .Substring(c.RegionLink.Name.Length - 3, 2)
                        .ToLower()
                        .Equals(_projectDetails.ManagerName.Region
                            .ToLower())
                    && c.SkillInventories.Any(d => d.SkillId == SelectedSkillRequirement.SkillId)
                    )
                    .Include(c => c.Assignments)
                    .ThenInclude(c => c.GcsTaskLink)
                ;

            var employee = query
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName)
                .Select(c => new EmployeeDetails(c))
                .ToList();

            AvailableEmployees.Clear();

            foreach (var item in employee) AvailableEmployees.Add(item);
        }

        private void LoadSkillsRequired()
        {
            foreach (var skills in SelectedTaskOnList.SkillsRequired)
            {
                SkillsRequired.Add(new SkillName()
                {
                    Description = skills.Description,
                    SkillId = skills.SkillId
                });
            }
        }

        public void AssignEmployee()
        {
            if (_selectedAvailableEmployee == null) return;
            
            var exists = SelectedTaskOnList.AssignedEmployees.FirstOrDefault(c => c.EmployeeId == SelectedAvailableEmployee.EmployeeId);
            if (exists != null) return;


            var count = SelectedTaskOnList.SkillsRequired.First(c => c.SkillId == SelectedSkillRequirement.SkillId)
                .Quantity;

            if (count > 0
                /*_addLimit + SelectedTaskOnList.AssignedEmployees.Count == count*/)
            {
                SelectedTaskOnList.AssignedEmployees.Add(SelectedAvailableEmployee);
                SelectedTaskOnList.AssignedEmployeesInString += $"{SelectedAvailableEmployee.Name}\n";
                SelectedTaskOnList.SkillsRequired.First(c => c.SkillId == SelectedSkillRequirement.SkillId)
                    .Quantity--;
                //_addLimit--;
            }

            var sb = new StringBuilder();

            foreach (var skillsRequired in SelectedTaskOnList.SkillsRequired)
            {
                //if (skillsRequired.SkillId == SelectedSkillRequirement.SkillId)
                //{
                //    sb.AppendLine(
                //        $"{skillsRequired.Quantity - SelectedTaskOnList.AssignedEmployees.Count} more {skillsRequired.Description} available.");

                //}else
                    sb.AppendLine(
                        $"{skillsRequired.Quantity} more {skillsRequired.Description} available.");

            }

            UnAssignedSkills = sb.ToString();

            OnPropertyChanged(nameof(SelectedTaskOnList));
            OnPropertyChanged(nameof(UnAssignedSkills));
            OnPropertyChanged(nameof(TaskList));
            _saveAction();
        }

        public void SaveAssignment()
        {
            var projectSchedule = new NewProjectSchedule()
            {
                Description = SelectedSchedule.Description,
                DateMade = DateTime.Now,
                ProjectId = _projectDetails.ProjectId,
            };
            foreach (var task in TaskList)
            {
                projectSchedule.Tasks.Add(task);
            }

            if (SelectedSchedule == null)
            {
                return;
            }
            UnAssignedSkills = "";
            OnPropertyChanged(nameof(UnAssignedSkills));

            var index = ProjectSchedules.IndexOf(SelectedSchedule);
            projectSchedule.DateMade = DateTime.Parse(SelectedSchedule.DateMade);
            ProjectSchedules[index] = new ProjectScheduleDetail(projectSchedule, _projectDetails.ManagerName);
        }

        public void RemoveAssignment()
        {
            if (_selectedTaskOnList == null) return;

            SelectedTaskOnList.AssignedEmployees.Clear();
            SelectedTaskOnList.AssignedEmployeesInString = "";
            OnPropertyChanged(nameof(SelectedTaskOnList));
            OnPropertyChanged(nameof(TaskList));
            //if (SelectedTaskOnList != null && SelectedSkillRequirement != null)
            //{
            //    _addLimit = SelectedTaskOnList.SkillsRequired
            //        .FirstOrDefault(c => c.SkillId == SelectedSkillRequirement.SkillId)
            //        .Quantity;
            //}
            UnAssignedSkills = "";
            OnPropertyChanged(nameof(UnAssignedSkills));
            _saveAction();
        }
        

        public void Finish(Page currentPage)
        {
            SaveProject();
            //_context.Dispose();
            var parent = Window.GetWindow(currentPage.Parent);
            parent.Close();
        }

        protected virtual void SaveProject()
        {
            var project = new Project()
            {
                ManagerId = _projectDetails.ManagerId,
                CustomerId = _projectDetails.CustomerId,
                Description = _projectDetails.Description,
                EstimatedDateStart = _projectDetails.EstimatedDateStart,
                EstimatedDateEnd = _projectDetails.EstimatedDateEnd,
                EstimatedBudget = _projectDetails.EstimatedBudget,
                DateStart = _projectDetails.DateStart,
                DateEnd = _projectDetails.DateEnd,
                DateSigned = _projectDetails.DateSigned
            };

            if (project.DateStart == DateTime.MinValue) project.DateStart = null;
            if (project.DateEnd == DateTime.MinValue) project.DateStart = null;
            
            project.ProjectSchedules = new List<ProjectSchedule>();

            foreach (var projectSchedule in _projectDetails.ProjectSchedules)
            {
                var newSchedule = new ProjectSchedule
                {
                    Description = projectSchedule.Description,
                    DateMade = projectSchedule.DateMade,
                    ProjectId = project.ProjectId
                };

                project.ProjectSchedules.Add(newSchedule);

                newSchedule.GcsTasks = new List<GcsTask>();

                foreach (var task in projectSchedule.Tasks)
                {
                    var newTask = new GcsTask
                    {
                        Description = task.Description,
                        DateEnd = task.DateEnd,
                        DateStart = task.DateStart,
                        ProjectScheduleId = newSchedule.ProjectScheduleId
                    };

                    newSchedule.GcsTasks.Add(newTask);

                    newTask.Assignments = new List<Assignment>();
                    newTask.TaskSkills = new List<TaskSkill>();

                    foreach (var assignedEmployee in task.AssignedEmployees)
                    {
                        newTask.Assignments.Add(new Assignment()
                        {
                            EmployeeId = assignedEmployee.EmployeeId,
                            GcsTaskId = newTask.GcsTaskId,
                        });
                    }

                    foreach (var skillsRequired in task.SkillsRequired)
                    {
                        newTask.TaskSkills.Add(new TaskSkill
                        {
                            GcsTaskId = newTask.GcsTaskId,
                            SkillId = skillsRequired.SkillId,
                            Quantity = skillsRequired.Quantity,
                        });
                    }
                }
            }

            _context.Add(project);
            try
            {
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.Message);
            }
        }

        public void NavigateBackwards(NavigationService nav, Page currentPage)
        {
            if (nav.CanGoBack)
            {
                nav.GoBack();
            }
            else
            {
                var parent = Window.GetWindow(currentPage.Parent);
                parent.Close();
            }
        }

        public void SetSaveAction(Action act)
        {
            _saveAction = act;
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
