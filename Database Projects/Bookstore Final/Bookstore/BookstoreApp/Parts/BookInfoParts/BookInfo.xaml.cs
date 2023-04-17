using BookstoreApp.ViewModels;
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
using BookstoreApp.Dto;

namespace BookstoreApp
{
    /// <summary>
    /// Interaction logic for BookInfo.xaml
    /// </summary>
    public partial class BookInfo : UserControl
    {
        public BookInfo()
        {
            InitializeComponent();
        }

        BookListViewModel _context;

        private void BookInfo_OnLoaded(object sender, RoutedEventArgs e)
        {
            cardBookDetails.Visibility = Visibility.Collapsed;

            _context = DataContext as BookListViewModel;

            _context.PropertyChanged += ContextOnPropertyChanged;

            Price.SetFormat("{0:N4}");
        }

        private void ContextOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_context.SelectedBook))
            {
                if (_context.SelectedBook == null)
                    cardBookDetails.Visibility = Visibility.Collapsed;
                else 
                    cardBookDetails.Visibility = Visibility.Visible;
            }
        }

        private void BtnOpenAddBook_OnClick(object sender, RoutedEventArgs e)
        {
            var addBookWindow = new AddBook();
            addBookWindow.Show();
        }
    }
}
