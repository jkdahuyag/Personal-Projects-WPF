using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Shapes;
using GlobalComputerSolutionsApp.Contexts;
using GlobalComputerSolutionsDb;
using Syncfusion.UI.Xaml.NavigationDrawer;

namespace GlobalComputerSolutionsApp.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
    {
        private GcsDbContext _context;
        public ObservableCollection<NavigationDrawerModel> Items { get; set; }
        public MainViewModel()
        {
            Items = new ObservableCollection<NavigationDrawerModel>
            {
                new ()
                {
                    Item = "Projects",
                    Icon = new Path()
                    {
                        
                    }
                },
                new ()
                {
                    Item = "Employees",
                    Icon = new Path()
                    {
                        
                    }
                },
                new ()
                {
                    Item = "Customers",
                    Icon = new Path()
                    {
                        
                    }
                }
            };
        }

        public UserControl ChangeContext(NavigationItem e)
        {
            var userControl = new UserControl();

            var header = e.Header as NavigationDrawerModel;
            if (header.Item == "Projects")
            {
                userControl = new ProjectsView();
                if (userControl.DataContext == null)
                {
                    var v = userControl as ProjectsView;
                    v.SetContext();
                }
            }
            else if (header.Item == "Employees")
            {
                userControl = new EmployeesView();
            }else if(header.Item == "Customers" )
            {
                userControl = new CustomersView();
            }

            return userControl;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
