using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Dahuyag_Final_Project_Software_Design.Annotations;
using Dahuyag_Final_Project_Software_Design.Dto;
using Dahuyag_Final_Project_Software_Design.Helpers;
using Dahuyag_Final_Project_Software_Design.SubWindows;
using Dahuyag_Final_Project_Software_Design.Validators;
using GlobalComputerSolutionsDb;
using GlobalComputerSolutionsDb.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Dahuyag_Final_Project_Software_Design.ViewModels
{
    public class AddProjectViewModel : INotifyPropertyChanged
    {
        protected GcsDbContext _context;

        private string _projectDescriptionInput;

        private float _projectEstimatedBudgetInput;

        private DateTime _estimatedDateStartInput;

        private DateTime _estimatedDateEndInput;

        private DateTime? _dateStartInput;

        private DateTime? _dateEndInput;

        private string _employeeSearchText;

        private string _customerSearchText;

        private EmployeeName _selectedEmployee;

        private CustomerName _selectedCustomer;

        private bool _canAddProject(object parameter) { return ErrorsInText.Length == 0; }

        private DateTime _dateSignedInput;

        public ICommand Next { get => new RelayCommand(SaveProject, _canAddProject); }

        protected ProjectDetails SavedProjectDetails;

        public string ErrorsInText { get; set; } = "";

        public DateTime DateSignedInput
        {
            get
            {
                return _dateSignedInput;
            }
            set
            {
                _dateSignedInput = value;
                OnPropertyChanged();
                CheckProject();
            }
        }

        public EmployeeName SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged();
                CheckProject();
            }
        }
        
        public CustomerName SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged();
                LoadEmployees();
                CheckProject();
            }
        }

        public string CustomerSearchText
        {
            get { return _customerSearchText; }
            set
            {
                _customerSearchText = value;
                LoadCustomers();
                OnPropertyChanged();
            }
        }

        public string EmployeeSearchText
        {
            get { return _employeeSearchText; }
            set
            {
                _employeeSearchText = value;
                LoadEmployees();
                OnPropertyChanged();
            }
        }

        public DateTime EstimatedDateEndInput
        {
            get { return _estimatedDateEndInput; }
            set
            {
                _estimatedDateEndInput = value;
                OnPropertyChanged();
                CheckProject();
            }
        }

        public DateTime EstimatedDateStartInput
        {
            get { return _estimatedDateStartInput; }
            set
            {
                _estimatedDateStartInput = value;
                OnPropertyChanged();
                CheckProject();
            }
        }
        
        public DateTime? DateEndInput
        {
            get { return _dateEndInput; }
            set
            {
                _dateEndInput = value;
                OnPropertyChanged();
                CheckProject();
            }
        }

        public DateTime? DateStartInput
        {
            get { return _dateStartInput; }
            set
            {
                _dateStartInput = value;
                OnPropertyChanged();
                CheckProject();
            }
        }

        public float ProjectEstimatedBudgetInput
        {
            get { return _projectEstimatedBudgetInput; }
            set
            {
                _projectEstimatedBudgetInput = value;
                OnPropertyChanged();
                CheckProject();
            }
        }

        public string ProjectDescriptionInput
        {
            get { return _projectDescriptionInput; }
            set
            {
                _projectDescriptionInput = value;
                OnPropertyChanged();
                CheckProject();
            }
        }

        public ObservableCollection<EmployeeName> Employees { get; set; } = new();
        public ObservableCollection<CustomerName> Customers { get; set; } = new();

        public AddProjectViewModel(GcsDbContext context)
        {
            _context = context;
        }

        public void LoadEmployees()
        {
            FilterEmployees();
        }

        private void FilterEmployees()
        {
            string search = EmployeeSearchText?.ToLowerInvariant() ?? string.Empty;

            var employees = new List<EmployeeName>();
            if (_selectedCustomer != null)
            {
                var query = _context.Employees
                        .Include(c => c.RegionLink)
                        .Where(c => (c.FirstName.ToLower().Contains(search)
                                     || c.LastName.ToLower().Contains(search)
                                     || c.MiddleName.ToLower().Contains(search))
                                    && c.RegionLink.Name.Substring(c.RegionLink.Name.Length-3,2).ToLower().Equals(_selectedCustomer.Region.ToLower()))
                    ;

                employees = query
                    .OrderBy(c => c.LastName)
                    .ThenBy(c => c.FirstName)
                    .Select(c => new EmployeeName(c))
                    .ToList();
            }
            else
            {
                var query = _context.Employees
                        .Include(c => c.RegionLink)
                        .Where(c => c.FirstName.ToLower().Contains(search)
                                     || c.LastName.ToLower().Contains(search)
                                     || c.MiddleName.ToLower().Contains(search))
                    ;

                employees = query
                    .OrderBy(c => c.LastName)
                    .ThenBy(c => c.FirstName)
                    .Select(c => new EmployeeName(c))
                    .ToList();
            }

           

            Employees.Clear();

            foreach (var item in employees) Employees.Add(item);
        }

        public void LoadCustomers()
        {
            FilterCustomers();
        }

        private void FilterCustomers()
        {
            string search = CustomerSearchText?.ToLowerInvariant() ?? string.Empty;

            var query = _context.Customers
                    .Include(c => c.RegionLink)
                    .Where(c => c.FirstName.ToLower().Contains(search)
                                || c.LastName.ToLower().Contains(search)
                                || c.MiddleName.ToLower().Contains(search)
                                || c.RegionLink.Name.ToLower().Contains(search))
                ;

            var customer = query
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName)
                .Select(c => new CustomerName(c))
                .ToList();

            Customers.Clear();

            foreach (var item in customer) Customers.Add(item);
        }

        public void CheckProject()
        {
            var validator = new ProjectValidator();

            var result = validator.Validate(this);

            var sb = new StringBuilder();

            foreach (var error in result.Errors)
            {
                sb.AppendLine(error.ErrorMessage);
            }

            ErrorsInText = sb.ToString();

            OnPropertyChanged(nameof(ErrorsInText));
            OnPropertyChanged(nameof(_canAddProject));
        }

        protected void SaveProject(object parameter)
        {
            if (SavedProjectDetails == null)
            {
                SavedProjectDetails = new ProjectDetails()
                {
                    Description = _projectDescriptionInput,
                    EstimatedBudget = _projectEstimatedBudgetInput,
                    EstimatedDateEnd = _estimatedDateEndInput,
                    EstimatedDateStart = _estimatedDateStartInput,
                    DateStart = _dateStartInput,
                    DateEnd = _dateEndInput,
                    DateSigned = _dateSignedInput,
                    ManagerId = _selectedEmployee.EmployeeId,
                    CustomerId = _selectedCustomer.CustomerId
                }; 
                SavedProjectDetails.ManagerName = SelectedEmployee;
                SavedProjectDetails.CustomerName = SelectedCustomer;

            }
            else
            {
                SavedProjectDetails.Description = _projectDescriptionInput;
                SavedProjectDetails.EstimatedBudget = _projectEstimatedBudgetInput;
                SavedProjectDetails.EstimatedDateEnd = _estimatedDateEndInput;
                SavedProjectDetails.EstimatedDateStart = _estimatedDateStartInput;
                SavedProjectDetails.DateStart = _dateStartInput;
                SavedProjectDetails.DateEnd = _dateEndInput;
                SavedProjectDetails.DateSigned = _dateSignedInput;
                SavedProjectDetails.ManagerId = _selectedEmployee.EmployeeId;
                SavedProjectDetails.CustomerId = _selectedCustomer.CustomerId;
                SavedProjectDetails.ManagerName = SelectedEmployee;
                SavedProjectDetails.CustomerName = SelectedCustomer;
            }
        }

        public virtual void NavigateForwards(NavigationService nav, Page currentPage)
        {
            object dummy = null;
            SaveProject(dummy);

            var nextPage = new ConfigureProjectSchedulePage();
            nextPage.DataContext = new AddProjectScheduleViewModel(_context,SavedProjectDetails);
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

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
