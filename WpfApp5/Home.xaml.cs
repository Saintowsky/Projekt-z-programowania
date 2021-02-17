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

namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz wyjść?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Close();
            }
           
        }

        private void BooksButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new BooksPage();
        }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new UsersPage();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void IssuesButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Wypożyczanie();
        }

        private void ReturnsButton_Click(object sender, RoutedEventArgs e)
        {
            ReturnBook rb = new ReturnBook();
            rb.Show();
        }

        private void StatsButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new StatsPage();
            
        }
    }
}
