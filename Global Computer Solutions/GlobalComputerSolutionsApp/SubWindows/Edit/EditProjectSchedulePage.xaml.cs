using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using GlobalComputerSolutionsApp.ViewModels;
using GlobalComputerSolutionsApp.ViewModels.Contents.Edit;

namespace GlobalComputerSolutionsApp.SubWindows.Edit
{
    /// <summary>
    /// Interaction logic for EditProjectSchedulePage.xaml
    /// </summary>
    public partial class EditProjectSchedulePage : Page
    {
        private static DependencyProperty _myPageDataProperty;

        static EditProjectSchedulePage()
        {
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata();
            metadata.Journal = true;
            _myPageDataProperty = DependencyProperty.Register(
                "_myPageDataProperty", typeof(string),
                typeof(EditProjectSchedulePage), metadata, null);
        }
        private string MyPageData
        {
            set { SetValue(_myPageDataProperty, value); }
            get { return (string)GetValue(_myPageDataProperty); }
        }

        public EditProjectSchedulePage()
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
            var viewModel = DataContext as EditProjectScheduleViewModel;
            viewModel.AddSkillToList();
            ICollectionView view = CollectionViewSource.GetDefaultView(LstVwSkills.ItemsSource);
            view.Refresh();
        }

        private void BtnRemoveFromSkillList_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as EditProjectScheduleViewModel;
            viewModel.RemoveSkillFromList();
            ICollectionView view = CollectionViewSource.GetDefaultView(LstVwSkills.ItemsSource);
            view.Refresh();
        }

        private void BtnUpSkillHierarchy_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as EditProjectScheduleViewModel;
            viewModel.IncreaseHierarchy();
        }

        private void BtnDownSkillHierarchy_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as EditProjectScheduleViewModel;
            viewModel.DecreaseHierarchy();
        }

        private void BtnRemoveFromTaskList_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as EditProjectScheduleViewModel;
            viewModel.RemoveTaskFromList();
            ICollectionView view = CollectionViewSource.GetDefaultView(LstVwTasks.ItemsSource);
            view.Refresh();

        }

        private void BtnEditTask_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as EditProjectScheduleViewModel;
            viewModel.LoadTaskDetails(viewModel.SelectedTaskOnList);
            ICollectionView view = CollectionViewSource.GetDefaultView(LstVwTasks.ItemsSource);
            view.Refresh();
        }

        private void BtnPrevPage_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);
            var viewModel = DataContext as EditProjectScheduleViewModel;
            viewModel.NavigateBackwards(nav, this);
        }

        private void BtnNextPage_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);
            var viewModel = DataContext as EditProjectScheduleViewModel;
            viewModel.NavigateForwards(nav, this);
        }

        private void BtnAddNewSchedule_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as EditProjectScheduleViewModel;
            viewModel.AddNewProjectSchedule();
        }

        private void BtnAddNewTask_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as EditProjectScheduleViewModel;
            viewModel.AddNewTask();
        }

        private void ConfigureProjectSchedulePage_OnLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as EditProjectScheduleViewModel;


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
