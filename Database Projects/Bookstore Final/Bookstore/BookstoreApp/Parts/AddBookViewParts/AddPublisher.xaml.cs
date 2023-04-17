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
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftAntimalwareAMFilter;

namespace BookstoreApp.Parts.AddBookViewParts
{
    /// <summary>
    /// Interaction logic for AddPublisher.xaml
    /// </summary>
    public partial class AddPublisher : UserControl
    {
        public AddPublisher()
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

    }
}
