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

namespace BookstoreApp
{
    /// <summary>
    /// Interaction logic for AddBookView.xaml
    /// </summary>
    public partial class AddBookView : UserControl
    {
        public AddBookView()
        {
            InitializeComponent();
        }

        private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AddBookViewModel;
            viewModel.SaveBook();

            MessageBox.Show("Book Successfully Added");

            var parent = Window.GetWindow(Parent);
            parent.Close();

        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(Parent);
            parent.Close();
        }

    }
}
