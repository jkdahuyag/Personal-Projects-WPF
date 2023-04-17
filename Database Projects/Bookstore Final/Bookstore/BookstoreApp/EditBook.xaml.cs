using BookstoreApp.ViewModels;
using BookStoreDb;
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

namespace BookstoreApp
{
    /// <summary>
    /// Interaction logic for EditBook.xaml
    /// </summary>
    public partial class EditBook : Window
    {
        public EditBook()
        {
            InitializeComponent();
        }

        private void BtnAddAuthor_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as EditBookViewModel;

            viewModel.AddAuthorToList();
        }

        private void BtnRemoveFromList_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as EditBookViewModel;

            viewModel.RemoveAuthorFromList();
        }

        private void PublisherTxtSearch_OnGotFocus(object sender, RoutedEventArgs e)
        {
            PublisherTblSearchBackground.Visibility = Visibility.Hidden;
        }

        private void PublisherTxtSearch_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PublisherTxtSearch.Text))
                PublisherTblSearchBackground.Visibility = Visibility.Hidden;
            else
                PublisherTblSearchBackground.Visibility = Visibility.Visible;
        }

        private void AuthorTxtSearch_OnGotFocus(object sender, RoutedEventArgs e)
        {
            AuthorTblSearchBackground.Visibility = Visibility.Hidden;
        }

        private void AuthorTxtSearch_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PublisherTxtSearch.Text))
                AuthorTblSearchBackground.Visibility = Visibility.Hidden;
            else
                AuthorTblSearchBackground.Visibility = Visibility.Visible;
        }


        private void BtnUpHierarchy_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as EditBookViewModel;

            viewModel.IncreaseHierarchy();
        }

        private void BtnDownHierarchy_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as EditBookViewModel;

            viewModel.DecreaseHierarchy();
        }

        private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as EditBookViewModel;

            viewModel.SaveBook();

            MessageBox.Show("Book Successfully Added");

            Close();
        }
        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EditBook_OnLoaded(object sender, RoutedEventArgs e)
        {
            var context = DataContext as EditBookViewModel;
            DtPDatePublished.DisplayDate = context.DatePublishedInput;
            DtPDatePublished.SelectedDate = context.DatePublishedInput;
        }
    }
}
