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
using Dahuyag_Final_Project_Software_Design.ViewModels;
using Syncfusion.UI.Xaml.Diagram;

namespace Dahuyag_Final_Project_Software_Design.SubWindows
{
    /// <summary>
    /// Interaction logic for ConfigureProjectSchedulePage.xaml
    /// </summary>
    public partial class ConfigureProjectSchedulePage : Page
    {
        private static DependencyProperty _myPageDataProperty;

        static ConfigureProjectSchedulePage()
        {
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata();
            metadata.Journal = true;
            _myPageDataProperty = DependencyProperty.Register(
                "_myPageDataProperty", typeof(string),
                typeof(ConfigureProjectSchedulePage), metadata, null);
        }
        private string MyPageData
        {
            set { SetValue(_myPageDataProperty, value); }
            get { return (string)GetValue(_myPageDataProperty); }
        }

        public ConfigureProjectSchedulePage()
        {
            InitializeComponent();
        }

        private void SkillTxtSearch_OnGotFocus(object sender, RoutedEventArgs e)
        {
            SkillTblSearchBackground.Visibility = Visibility.Hidden;
        }

        private void SkillTxtSearch_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SkillTxtSearch.Text))
                SkillTblSearchBackground.Visibility = Visibility.Hidden;
            else
                SkillTblSearchBackground.Visibility = Visibility.Visible;
        }

        private void BtnAddSkill_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AddProjectScheduleViewModel;
            viewModel.AddSkillToList();
            ICollectionView view = CollectionViewSource.GetDefaultView(LstVwSkills.ItemsSource);
            view.Refresh();
        }

        private void BtnRemoveFromSkillList_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AddProjectScheduleViewModel;
            viewModel.RemoveSkillFromList();
            ICollectionView view = CollectionViewSource.GetDefaultView(LstVwSkills.ItemsSource);
            view.Refresh();
        }

        private void BtnUpSkillHierarchy_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AddProjectScheduleViewModel;
            viewModel.IncreaseHierarchy();
        }

        private void BtnDownSkillHierarchy_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AddProjectScheduleViewModel;
            viewModel.DecreaseHierarchy();
        }

        private void BtnRemoveFromTaskList_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AddProjectScheduleViewModel;
            viewModel.RemoveTaskFromList();
            ICollectionView view = CollectionViewSource.GetDefaultView(LstVwTasks.ItemsSource);
            view.Refresh();

        }

        private void BtnEditTask_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AddProjectScheduleViewModel;
            viewModel.LoadTaskDetails(viewModel.SelectedTaskOnList);
            ICollectionView view = CollectionViewSource.GetDefaultView(LstVwTasks.ItemsSource);
            view.Refresh();
        }

        private void BtnPrevPage_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);
            var viewModel = DataContext as AddProjectScheduleViewModel;
            viewModel.NavigateBackwards(nav,this);
        }

        private void BtnNextPage_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);
            var viewModel = DataContext as AddProjectScheduleViewModel;
            viewModel.NavigateForwards(nav, this);
        }

        private void BtnAddNewSchedule_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AddProjectScheduleViewModel;
            viewModel.AddNewProjectSchedule();
        }

        private void BtnAddNewTask_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AddProjectScheduleViewModel;
            viewModel.AddNewTask();
        }

        private void ConfigureProjectSchedulePage_OnLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AddProjectScheduleViewModel;

            
            viewModel.LoadSkills();
            DtpTaskDateEnd.SelectedDate = DateTime.Now;
            DtpTaskDateStart.SelectedDate = DateTime.Now;
            DtpTaskDateEnd.SelectedDate = null;
            DtpTaskDateStart.SelectedDate = null;
            viewModel.CheckProjectSchedule();
            viewModel.CheckTask();
            viewModel.CheckProjectScheduleList();
        }

        private void BtnSaveSchedule_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AddProjectScheduleViewModel;
            //await Task.Delay(500);

            //ICollectionView view = CollectionViewSource.GetDefaultView(LstBxSchedules.ItemsSource);
            //view.Refresh();
            var act = () =>
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(LstBxSchedules.ItemsSource);
                view.Refresh();
            };
            viewModel.SetSaveAction(act);
        }

        private void BtnAddTask_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AddProjectScheduleViewModel;

            //await Task.Delay(500);
            //ICollectionView view = CollectionViewSource.GetDefaultView(LstVwTasks.ItemsSource);
            //view.Refresh();

            var act = () =>
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(LstVwTasks.ItemsSource);
                view.Refresh();
            };
            viewModel.SetSaveAction(act);
        }
    }
}
