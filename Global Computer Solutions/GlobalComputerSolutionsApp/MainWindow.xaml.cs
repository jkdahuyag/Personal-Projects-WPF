using System.Windows;
using GlobalComputerSolutionsApp.ViewModels;
using GlobalComputerSolutionsApp.ViewModels.Contents;
using GlobalComputerSolutionsDb;
using Syncfusion.SfSkinManager;
using Syncfusion.UI.Xaml.NavigationDrawer;

namespace GlobalComputerSolutionsApp
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
