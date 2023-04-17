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
using BookstoreApp.ViewModels;
using BookStoreDb;

namespace BookstoreApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BookListViewModel _bookListViewModel;
        private BookStoreLiteContext _context;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _context = new BookStoreLiteContext();
            _bookListViewModel = new BookListViewModel(_context);
            DataContext = _bookListViewModel;

            _bookListViewModel.LoadBooks();
        }

        private void MainWindow_OnClosed(object? sender, EventArgs e)
        {
            _context.Dispose();
        }

        private void RbnBtnAddBook_OnClick(object sender, RoutedEventArgs e)
        {
            _bookListViewModel.CreateNewBook();
        }

        private void RbnBtnEditBook_OnClick(object sender, RoutedEventArgs e)
        {
            _bookListViewModel.EditBook();
        }

        private void RbnBtnRemoveBook_OnClick(object sender, RoutedEventArgs e)
        {
            _bookListViewModel.RemoveBook();
        }
    }
}
