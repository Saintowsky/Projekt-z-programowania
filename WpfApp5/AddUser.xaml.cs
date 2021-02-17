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
using System.Data.SqlClient;
using System.Data;


namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        SqlConnection con = new SqlConnection(@"Data Source=(local);Initial Catalog=Library;Integrated Security=True");
        DBAccess objDBAccess = new DBAccess();
        DataTable dtBooks = new DataTable();
        public AddUser()
        {
            InitializeComponent();
        }

        private void ButtonAdd(object sender, RoutedEventArgs e)
        {
            string Name = TextName.Text;
            string Surname = TextSurname.Text;
            string Email = TextEmail.Text;
            string Contact = TextContact.Text;
            string PESEL = TextPesel.Text;

            if (Name.Equals(""))
            {
                MessageBox.Show("Wprowadź imię!");
            }
            else if (Surname.Equals(""))
            {
                MessageBox.Show("Wprowadź Nazwisko!");
            }
            else if (Email.Equals(""))
            {
                MessageBox.Show("Wprowadź email!");
            }
            else if (Contact.Equals(""))
            {
                MessageBox.Show("Wprowadź numer telefonu!");
            }
            else if (PESEL.Equals(""))
            {
                MessageBox.Show("Wprowadź PESEL!");
            }
            else
            {
                
                SqlCommand insertCommand = new SqlCommand("insert into Users(Name,Surname,Email,Contact,PESEL) values(@Name, @Surname, @Email, @Contact,@PESEL)");



                insertCommand.Parameters.AddWithValue("@Name", Name);
                insertCommand.Parameters.AddWithValue("@Surname", Surname);
                insertCommand.Parameters.AddWithValue("@Email", Email);
                insertCommand.Parameters.AddWithValue("@Contact", Contact);
                insertCommand.Parameters.AddWithValue("@PESEL", PESEL);

                int row = objDBAccess.executeQuery(insertCommand);

                if (row == 1)
                {
                    MessageBox.Show("Konto zostało utworzone!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("BŁĄD!");
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz anulować akcję?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
