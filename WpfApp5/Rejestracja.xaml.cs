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
using System.Data;
using System.Data.SqlClient;

namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Rejestracja : Window
    {

        DBAccess objDBAccess = new DBAccess();
        public Rejestracja()
        {
            InitializeComponent();
        }

        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            string userName = txtName.Text;
            string userSurname = txtSurname.Text;
            string userEmail = txtEmail.Text;
            string userPassword = txtPassword.Password.ToString();

            if (userName.Equals(""))
            {
                MessageBox.Show("Wprowadź imię!");
            }
            else if (userSurname.Equals(""))
            {
                MessageBox.Show("Wprowadź Nazwisko!");
            }
            else if (userEmail.Equals(""))
            {
                MessageBox.Show("Wprowadź email!");
            }
            else if (userPassword.Equals(""))
            {
                MessageBox.Show("Wprowadź hasło!");
            }
            else
            {
                SqlCommand insertCommand = new SqlCommand("insert into Workers(Name,Surname,Email,Password) values(@userName, @userSurname, @userEmail, @userPassword)");

                insertCommand.Parameters.AddWithValue("@userName", userName);
                insertCommand.Parameters.AddWithValue("@userEmail", userEmail);
                insertCommand.Parameters.AddWithValue("@userPassword", userPassword);
                insertCommand.Parameters.AddWithValue("@userSurname", userSurname);

                int row = objDBAccess.executeQuery(insertCommand);

                if (row == 1)
                {
                    MessageBox.Show("Konto zostało utworzone!");

                    this.Hide();
                    Logowanie login = new Logowanie();
                    login.Show();
                }
                else
                {
                    MessageBox.Show("BŁĄD!");
                }
            }
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

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz anulować rejestrację?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
            AdminWindow aw = new AdminWindow();
            aw.Show();

        }
    }
}
