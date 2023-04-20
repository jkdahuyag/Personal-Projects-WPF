using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using GlobalComputerSolutionsApp.ViewModels;
using GlobalComputerSolutionsApp.ViewModels.Contents.Edit;

namespace GlobalComputerSolutionsApp.SubWindows.Edit
{
    /// <summary>
    /// Interaction logic for EditProjectPage.xaml
    /// </summary>
    public partial class EditProjectPage : Page
    {
        private static DependencyProperty _myPageDataProperty;

        static EditProjectPage()
        {
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata();
            metadata.Journal = true;
            _myPageDataProperty = DependencyProperty.Register(
                "_myPageDataProperty", typeof(string),
                typeof(EditProjectPage), metadata, null);
        }
        private string MyPageData
        {
            set { SetValue(_myPageDataProperty, value); }
            get { return (string)GetValue(_myPageDataProperty); }
        }

        public EditProjectPage()
        {
            InitializeComponent();
        }
        private void EmployeeTxtSearch_OnGotFocus(object sender, RoutedEventArgs e)
        {
            EmployeeTblSearchBackground.Visibility = Visibility.Hidden;
        }

        private void EmployeeTxtSearch_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EmployeeTxtSearch.Text))
                EmployeeTblSearchBackground.Visibility = Visibility.Hidden;
            else
                EmployeeTblSearchBackground.Visibility = Visibility.Visible;
        }

        private void CustomerTxtSearch_OnGotFocus(object sender, RoutedEventArgs e)
        {
            CustomerTblSearchBackground.Visibility = Visibility.Hidden;
        }

        private void CustomerTxtSearch_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(CustomerTblSearchBackground.Text))
                CustomerTblSearchBackground.Visibility = Visibility.Hidden;
            else
                CustomerTblSearchBackground.Visibility = Visibility.Visible;
        }

        private void BtnNext_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);
            var viewModel = DataContext as EditProjectViewModel;
            viewModel.NavigateForwards(nav, this);
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(Parent);
            parent.Close();
        }

        private void ConfigureProjectPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AddProjectViewModel;
            if (viewModel.SelectedCustomer == null || viewModel.SelectedEmployee == null)
            {
                viewModel.LoadCustomers();
                viewModel.LoadEmployees();
                DtpEstimateDateEnd.SelectedDate = DateTime.Now;
                DtpEstimateDateStart.SelectedDate = DateTime.Now;
                DtpEstimateDateEnd.DisplayDate = DateTime.Now;
                DtpEstimateDateStart.DisplayDate = DateTime.Now;
                DtpDateEnd.SelectedDate = null;
                DtpDateStart.SelectedDate = null;
                DtpDateEnd.DisplayDate = DateTime.Now;
                DtpDateStart.DisplayDate = DateTime.Now;
                DtpDateSigned.SelectedDate = DateTime.Now;
                DtpDateSigned.DisplayDate = DateTime.Now;
            }
        }
    }
}
