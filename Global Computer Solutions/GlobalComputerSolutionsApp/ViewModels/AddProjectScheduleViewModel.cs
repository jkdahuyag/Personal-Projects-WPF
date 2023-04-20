using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using GlobalComputerSolutionsApp.Dto;
using GlobalComputerSolutionsApp.Helpers;
using GlobalComputerSolutionsApp.SubWindows;
using GlobalComputerSolutionsApp.Validators;
using GlobalComputerSolutionsDb;
using Microsoft.EntityFrameworkCore;

namespace GlobalComputerSolutionsApp.ViewModels
{
    public class AddProjectScheduleViewModel:INotifyPropertyChanged
    {
        protected GcsDbContext _context;

        protected ProjectDetails _projectDetails;

        private ProjectScheduleDetail _selectedProjectSchedule;

        private string _projectScheduleDescriptionInput;

        private TaskDetails _selectedTaskOnList;

        private string _taskDescriptionInput;

        private DateTime _estimatedDateStartInput;

        private DateTime _estimatedDateEndInput;

        private SkillDetails _selectedSkill;

        private SkillDetails _selectedSkillInList;

        public string TaskErrorsInText { get; set; } = "";

        public string ProjectScheduleErrorsInText { get; set; } = "";

        public string ProjectScheduleListErrorsInText { get; set; } = "";

        private bool _canAddProjectSchedule(object parameter) { return ProjectScheduleErrorsInText.Length == 0; }

        public ICommand SaveProjectSchedule { get => new RelayCommand(AddProjectSchedule, _canAddProjectSchedule); }

        private bool _canAddTask(object parameter) { return TaskErrorsInText.Length == 0; }

        public ICommand SaveTask { get => new RelayCommand(AddTask, _canAddTask); }
        
        private bool _canAddProject(object parameter) { return ProjectScheduleListErrorsInText.Length == 0; }

        public ICommand Next { get => new RelayCommand(SaveProject, _canAddProject); }

        private string _skillSearchText;

        private Action _saveAction;

        public string SkillSearchText
        {
            get { return _skillSearchText; }
            set { _skillSearchText = value; }
        }


        public SkillDetails SelectedSkillOnList
        {
            get { return _selectedSkillInList; }
            set
            {
                _selectedSkillInList = value; 
                OnPropertyChanged();
            }
        }

        public SkillDetails SelectedSkill
        {
            get { return _selectedSkill; }
            set
            {
                _selectedSkill = value; 
                OnPropertyChanged();
                CheckTask();
            }
        }

        public DateTime EstimatedDateEndInput
        {
            get { return _estimatedDateEndInput; }
            set
            {
                _estimatedDateEndInput = value;
                OnPropertyChanged();
                CheckTask();
            }
        }

        public DateTime EstimatedDateStartInput
        {
            get { return _estimatedDateStartInput; }
            set
            {
                _estimatedDateStartInput = value; 
                OnPropertyChanged();
                CheckTask();
            }
        }

        public string TaskDescriptionInput
        {
            get { return _taskDescriptionInput; }
            set
            {
                _taskDescriptionInput = value; 
                OnPropertyChanged();
                CheckTask();
            }
        }

        public TaskDetails SelectedTaskOnList
        {
            get { return _selectedTaskOnList; }
            set
            {
                _selectedTaskOnList = value; 
                OnPropertyChanged();
                LoadTaskDetails(value);
                CheckTask();
            }
        }

        public string ProjectScheduleDescriptionInput
        {
            get { return _projectScheduleDescriptionInput; }
            set
            {
                _projectScheduleDescriptionInput = value; 
                OnPropertyChanged();
                CheckProjectSchedule();
            }
        }

        public ProjectScheduleDetail SelectedProjectSchedule
        {
            get { return _selectedProjectSchedule; }
            set
            {
                _selectedProjectSchedule = value; 
                LoadProjectScheduleDetails(value);
                OnPropertyChanged();
                CheckProjectSchedule();
            }
        }

        public ObservableCollection<SkillDetails> Skills { get; set; } = new();

        public ObservableCollection<SkillDetails> SkillList { get; set; } = new();

        public ObservableCollection<TaskDetails> TaskList { get; set; } = new();

        public ObservableCollection<ProjectScheduleDetail> ProjectSchedules { get; set; } = new();

        public AddProjectScheduleViewModel(GcsDbContext context, ProjectDetails projectDetails)
        {
            _context = context;
            _projectDetails = projectDetails;

        }

        public void CheckProjectSchedule()
        {
            var validator = new ProjectScheduleValidator();

            var result = validator.Validate(this);

            var sb = new StringBuilder();

            foreach (var error in result.Errors)
            {
                sb.AppendLine(error.ErrorMessage);
            }
            ProjectScheduleErrorsInText = sb.ToString();

            OnPropertyChanged(nameof(ProjectScheduleErrorsInText));
            //OnPropertyChanged(nameof(_canAddProjectSchedule));
        }

        public void CheckTask()
        {
            var validator = new TaskValidator();

            var result = validator.Validate(this);

            var sb = new StringBuilder();

            foreach (var error in result.Errors)
            {
                sb.AppendLine(error.ErrorMessage);
            }
            TaskErrorsInText = sb.ToString();

            OnPropertyChanged(nameof(TaskErrorsInText));
            //OnPropertyChanged(nameof(_canAddTask));
        }

        public void CheckProjectScheduleList()
        {
            var validator = new ProjectScheduleListValidator();

            var result = validator.Validate(this);

            var sb = new StringBuilder();

            foreach (var error in result.Errors)
            {
                sb.AppendLine(error.ErrorMessage);
            }
            ProjectScheduleListErrorsInText = sb.ToString();

            OnPropertyChanged(nameof(ProjectScheduleListErrorsInText));
            //OnPropertyChanged(nameof(_canAddProject));
        }


        public virtual void LoadProjectScheduleDetails(ProjectScheduleDetail p)
        {
            if (p == null)
            {
                return;
            }
            ProjectScheduleDescriptionInput = p.Description;
            TaskList.Clear();
            foreach (var pTask in p.Tasks)
            {
                TaskList.Add(pTask);
            }
        }

        public virtual void LoadTaskDetails(TaskDetails t)
        {
            if (t == null)
            {
                return;
            }
            TaskDescriptionInput = t.Description;
            EstimatedDateStartInput = t.DateStart;
            EstimatedDateEndInput = t.DateEnd;
            SkillList.Clear();
            foreach (var skill in t.SkillsRequired)
            {
                SkillList.Add(skill);
            }
        }

        public void LoadSkills()
        {
            FilterSkills();
        }

        private void FilterSkills()
        {
            string search = SkillSearchText?.ToLowerInvariant() ?? string.Empty;

            var query = _context.Skills
                    .Include(c=>c.TaskSkills)
                    .Where(c => c.Description.ToLower().Contains(search))
                ;

            var skill = query
                .OrderBy(c => c.Description)
                .ThenBy(c => c.PayRate)
                .Select(c => new SkillDetails(c))
            .ToList();

            Skills.Clear();

            foreach (var item in skill) Skills.Add(item);
        }

        public void AddNewProjectSchedule()
        {
            ProjectSchedules.Add(new ProjectScheduleDetail()
            {
                Description = "Sample Schedule",
                ManagerName = "Edit this to continue"
            });
            CheckProjectScheduleList();
        }

        public void AddNewTask()
        {
            TaskList.Add(new TaskDetails()
            {
                Description = "Edit this to continue",
                DateStart = DateTime.Now,
                DateEnd = DateTime.Now
            });
            CheckProjectSchedule();
        }

        public void AddSkillToList()
        {
            if (_selectedSkill == null) return;

            var exists = SkillList.FirstOrDefault(c => c.SkillId == SelectedSkill.SkillId);
            if (exists != null)
            {
                _selectedSkill.Quantity++;
                OnPropertyChanged(nameof(_selectedSkill.Quantity));
                return;
            }
            SkillList.Add(SelectedSkill);
            CheckTask();
        }

        public void RemoveSkillFromList()
        {
            if (_selectedSkillInList == null) return;

            if (_selectedSkillInList.Quantity > 1)
            {
                _selectedSkillInList.Quantity --;
                OnPropertyChanged(nameof(SelectedSkillOnList.Quantity));
                return;
            }

            OnPropertyChanged(nameof(SelectedSkillOnList));
            SkillList.Remove(SelectedSkillOnList);
            CheckTask();
        }

        public void RemoveTaskFromList()
        {
            if (_selectedTaskOnList == null) return;

            TaskList.Remove(_selectedTaskOnList);
            CheckProjectSchedule();
        }

        public void IncreaseHierarchy()
        {
            if (_selectedSkillInList == null) return;

            var index = SkillList.IndexOf(_selectedSkillInList);

            if (index == 0) return;

            SkillList.Move(index, index - 1);
        }
        public void DecreaseHierarchy()
        {
            if (_selectedSkillInList == null) return;

            var index = SkillList.IndexOf(_selectedSkillInList);

            if (index == SkillList.Count - 1) return;
            SkillList.Move(index, index + 1);
        }
        protected virtual void AddProjectSchedule(object obj)
        {
            var projectSchedule = new NewProjectSchedule()
            {
                Description = _projectScheduleDescriptionInput,
                DateMade = DateTime.Now,
                ProjectId = _projectDetails.ProjectId,
            };
            foreach (var task in TaskList)
            {
                projectSchedule.Tasks.Add(task);
            }

            if (SelectedProjectSchedule == null)
            {
                ProjectSchedules.Add(new ProjectScheduleDetail(projectSchedule,_projectDetails.ManagerName));
            }
            else
            {
                projectSchedule.ProjectScheduleId = SelectedProjectSchedule.ProjectScheduleId;
                var index = ProjectSchedules.IndexOf(SelectedProjectSchedule);
                projectSchedule.DateMade = DateTime.Parse(SelectedProjectSchedule.DateMade);
                ProjectSchedules[index] = new ProjectScheduleDetail(projectSchedule, _projectDetails.ManagerName);
            }
            _saveAction();
            CheckProjectScheduleList();
            ClearProjectScheduleInputs();
        }

        protected virtual void AddTask(object obj)
        {
            var task = new TaskDetails()
            {
                Description = _taskDescriptionInput,
                DateStart = _estimatedDateStartInput,
                DateEnd = _estimatedDateEndInput,
            };
            foreach (var skill in SkillList)
            {
                task.SkillsRequired.Add(new SkillDetails(skill));
                skill.Quantity = 1;
            }

            if (SelectedTaskOnList == null)
            {
                TaskList.Add(task);
            }
            else
            {
                var index = TaskList.IndexOf(SelectedTaskOnList);
                TaskList[index] = task;
            }
            CheckProjectSchedule();
            _saveAction();
            ClearTaskInputs();
        }

        protected void SaveProject(object obj)
        {
            _projectDetails.ProjectSchedules.Clear();
            foreach (var projectScheduleDetail in ProjectSchedules)
            {
                _projectDetails.ProjectSchedules.Add(new NewProjectSchedule()
                {
                    ProjectScheduleId = projectScheduleDetail.ProjectScheduleId,
                    Description = projectScheduleDetail.Description,
                    DateMade = DateTime.Parse(projectScheduleDetail.DateMade),
                    ProjectId = _projectDetails.ProjectId,
                    Tasks = projectScheduleDetail.Tasks
                });
            }
        }

        public virtual void NavigateForwards(NavigationService nav, Page currentPage)
        {
            object dummy = null;

            SaveProject(dummy);
            
            var nextPage = new ConfigureAssignmentsPage();
            nextPage.DataContext = new AddAssignmentsViewModel(_context,_projectDetails);
            nav.Navigate(nextPage);
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

        private void ClearTaskInputs()
        {
            TaskDescriptionInput = "";
            EstimatedDateStartInput = DateTime.Now;
            EstimatedDateEndInput = DateTime.Now;
            SelectedSkill = null;
            SkillList.Clear();
        }

        private void ClearProjectScheduleInputs()
        {
            ProjectScheduleDescriptionInput = "";
            TaskList.Clear();
            ClearTaskInputs();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
