using Dahuyag_Final_Project_Software_Design.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Dahuyag_Final_Project_Software_Design.ViewModels.Contents.Edit;

namespace Dahuyag_Final_Project_Software_Design.SubWindows.Edit
{
    /// <summary>
    /// Interaction logic for EditAssignmentsPage.xaml
    /// </summary>
    public partial class EditAssignmentsPage : Page
    {
        public EditAssignmentsPage()
        {
            InitializeComponent();
        }
        private void BtnRemoveAssignment_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as EditAssignmentsViewModel;
            viewModel.RemoveAssignment();
        }

        private void AvailableEmployeeTxtSearch_OnGotFocus(object sender, RoutedEventArgs e)
        {
            AvailableEmployeeTblSearchBackground.Visibility = Visibility.Hidden;
        }

        private void AvailableEmployeeTxtSearch_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(AvailableEmployeeTxtSearch.Text))
                AvailableEmployeeTblSearchBackground.Visibility = Visibility.Hidden;
            else
                AvailableEmployeeTblSearchBackground.Visibility = Visibility.Visible;
        }

        private void BtnAssignEmployee_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as EditAssignmentsViewModel;


            var act = () =>
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(LstVwTasks.ItemsSource);
                view.Refresh();
            };
            viewModel.SetSaveAction(act);
            viewModel.AssignEmployee();
        }

        private void BtnPrevPage_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);
            var viewModel = DataContext as EditAssignmentsViewModel;
            viewModel.NavigateBackwards(nav, this);
        }

        private void BtnFinish_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as EditAssignmentsViewModel;
            viewModel.Finish(this);
        }

        private void ConfigureAssignmentsPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as EditAssignmentsViewModel;
            viewModel.LoadProjectSchedules();
        }

        private void BtnAddAssignment_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as EditAssignmentsViewModel;
            viewModel.SaveAssignment();
        }
    }
}
