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

namespace BookstoreApp.Parts.AddBookViewParts
{
    /// <summary>
    /// Interaction logic for AddAuthor.xaml
    /// </summary>
    public partial class AddAuthor : UserControl
    {
        public AddAuthor()
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

        private void BtnRemoveFromList_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AddBookViewModel;
            viewModel.RemoveAuthorFromList();
        }

        private void BtnUpHierarchy_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AddBookViewModel;
            viewModel.IncreaseHierarchy();
        }

        private void BtnDownHierarchy_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AddBookViewModel;
            viewModel.DecreaseHierarchy();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AddBookViewModel;
            viewModel.AddAuthorToList();
        }
    }
}
