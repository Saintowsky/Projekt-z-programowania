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
using System.Data;
using System.Data.SqlClient;

namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for Logowanie.xaml
    /// </summary>
    public partial class Logowanie : Window
    {

        DBAccess objDBAccess = new DBAccess();
        DataTable dtUsers = new DataTable();

        public Logowanie()
        {
            InitializeComponent();
        }

        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmailLogin.Text;
            string password = txtPasswordLogin.Text;

            if (email.Equals(""))
            {
                MessageBox.Show("Proszę wpisać email!");
            }
            else if (password.Equals(""))
            {
                MessageBox.Show("Proszę wpisać hasło!");
            }
            else
            {
                string query = "Select * from Workers Where Email= '" + email + "' AND Password = '" + password + "' AND IsAdmin = 1";
                objDBAccess.readDatathroughAdapter(query, dtUsers);

                if (dtUsers.Rows.Count == 1)
                {
                    MessageBox.Show("Zostałeś zalogowany jako administrator!");
                    objDBAccess.closeConn();

                    this.Hide();
                    AdminWindow adminHome = new AdminWindow();
                    adminHome.Show();
                }
                else
                {
                    string query2 = "Select * from Workers Where Email= '" + email + "' AND Password = '" + password + "'";
                    objDBAccess.readDatathroughAdapter(query2, dtUsers);

                    if (dtUsers.Rows.Count == 1)
                    {
                        MessageBox.Show("Zostałeś zalogowany!");
                        objDBAccess.closeConn();

                        this.Hide();
                        Home home = new Home();
                        home.Show();
                    }
                    else
                    {
                        MessageBox.Show("Nie znaleziono konta!");
                    }
                }
            }
           
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Rejestracja rejestracja = new Rejestracja();
            rejestracja.Show();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz wyjść?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
