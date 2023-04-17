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
using Dahuyag_Final_Project_Software_Design.ViewModels;
using Dahuyag_Final_Project_Software_Design.ViewModels.Contents;
using GlobalComputerSolutionsDb;
using Microsoft.EntityFrameworkCore;
using Syncfusion.SfSkinManager;
using Syncfusion.UI.Xaml.NavigationDrawer;

namespace Dahuyag_Final_Project_Software_Design
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _mainViewModel = new();
        private GcsDbContext _context = new();

        public MainWindow()
        {
            SfSkinManager.ApplyStylesOnApplication = true;
            InitializeComponent();
        }

        private void NavigationDrawer_OnItemClicked(object? sender, NavigationItemClickedEventArgs e)
        {
            var newContext = _mainViewModel.ChangeContext(e.Item);
            UscContext.Content = newContext.Content;
            UscContext.DataContext = newContext.DataContext;
            if (UscContext.DataContext is ProjectsViewModel newViewModel)
            {
                newViewModel.LoadProjects();
            }
        }

        /*private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _context = new GcsDbContext();
            DataContext = _mainViewModel;
        }*/
        private void MainWindow_Onloaded(object sender, RoutedEventArgs e)
        {
            DataContext = _mainViewModel;
        }
    }
}
