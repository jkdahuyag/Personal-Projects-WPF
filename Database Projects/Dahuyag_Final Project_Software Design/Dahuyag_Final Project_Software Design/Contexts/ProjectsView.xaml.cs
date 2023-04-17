using System;
using System.Collections.Generic;
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
using Dahuyag_Final_Project_Software_Design.Dto;
using Dahuyag_Final_Project_Software_Design.ViewModels.Contents;
using GlobalComputerSolutionsDb;

namespace Dahuyag_Final_Project_Software_Design.Contexts
{
    /// <summary>
    /// Interaction logic for ProjectsView.xaml
    /// </summary>
    public partial class ProjectsView : UserControl
    {
        private static GcsDbContext _context = new();
        private ProjectsViewModel _viewModel;

        public ProjectsView()
        {
            InitializeComponent();
        }
        private void ProjectsCardTxtSearch_OnGotFocus(object sender, RoutedEventArgs e)
        {
            ProjectsCardSearchBackground.Visibility = Visibility.Hidden;
        }

        private void ProjectsCardTxtSearch_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ProjectsCardTxtSearch.Text))
                ProjectsCardSearchBackground.Visibility = Visibility.Hidden;
            else
                ProjectsCardSearchBackground.Visibility = Visibility.Visible;
        }
        
        public void SetContext()
        {
            _context = new GcsDbContext();
            _viewModel = new ProjectsViewModel(_context);
            DataContext = _viewModel;
            CmbNoOfProjects.SelectedIndex = 0;
        }

        private void BtnAddProject_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ProjectsViewModel;
            viewModel.CreateProject();
        }

        private void BtnEditProject_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ProjectsViewModel;
            viewModel.EditProject();
        }

        private void BtnRemoveProject_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ProjectsViewModel;
            viewModel.RemoveProject();
        }

        private void CrdVwProjects_OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewModel = DataContext as ProjectsViewModel;

            viewModel.SelectedCardDetails = e.NewValue as ProjectCardDetails;
        }
    }
}
