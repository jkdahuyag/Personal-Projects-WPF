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
using BookstoreApp.Helpers;
using BookstoreApp.ViewModels;

namespace BookstoreApp.Parts.BookInfo
{
    /// <summary>
    /// Interaction logic for BookList.xaml
    /// </summary>
    public partial class BookList : UserControl
    {
        public BookList()
        {
            InitializeComponent();
        }

        private void TxtSearch_OnGotFocus(object sender, RoutedEventArgs e)
        {
            TblSearchBackground.Visibility = Visibility.Hidden;
        }

        private void TxtSearch_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtSearch.Text))
                TblSearchBackground.Visibility = Visibility.Hidden;
            else
                TblSearchBackground.Visibility = Visibility.Visible;

        }

        Pagination _pageContext;
        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {


        }

        private void BtnNext_OnClick(object sender, RoutedEventArgs e)
        {
            _pageContext.NextPage();
        }

        private void BtnPrev_OnClick(object sender, RoutedEventArgs e)
        {
            _pageContext.PrevPage();
        }

        private void BookList_OnLoaded(object sender, RoutedEventArgs e)
        {
            var s = DataContext as BookListViewModel;
            _pageContext = s.PageDetails;

        }
    }
}
