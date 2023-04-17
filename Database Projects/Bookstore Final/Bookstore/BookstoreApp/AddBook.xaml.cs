using BookstoreApp.ViewModels;
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
using System.Windows.Shapes;
using BookStoreDb;

namespace BookstoreApp
{
    /// <summary>
    /// Interaction logic for AddBook.xaml
    /// </summary>
    public partial class AddBook : Window
    {
        public AddBook()
        {
            InitializeComponent();
        }

        private static BookStoreLiteContext _context = new BookStoreLiteContext(); 
        private static AddBookViewModel _viewModel = new AddBookViewModel(_context);

        private void AddBookView_OnLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = _viewModel;

            _viewModel.LoadAuthors();
            _viewModel.LoadPublishers();
        }

    }
}
