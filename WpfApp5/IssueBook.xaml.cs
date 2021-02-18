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
    /// Interaction logic for IssueBook.xaml
    /// </summary>
    public partial class IssueBook : Window
    {

        SqlConnection con = new SqlConnection(@"Data Source=(local);Initial Catalog=Library;Integrated Security=True");
        DBAccess objDBAccess = new DBAccess();
        DataTable dtBooks = new DataTable();
        public IssueBook()
        {
            InitializeComponent();
        }



        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz anulować akcję?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            cmd = new SqlCommand("select Name from Books", con);
            SqlDataReader Sdr = cmd.ExecuteReader();
            while (Sdr.Read())
            {
                for (int i = 0; i < Sdr.FieldCount; i++)
                {
                    Books.Items.Add(Sdr.GetString(i));
                }
            }
            Sdr.Close();
            con.Close();

        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TextPesel.Text != "")
            {
                String PESEL = TextPesel.Text;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select * from Users where PESEL ='" + PESEL + "'";
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet DS = new DataSet();
                sda.Fill(DS);
;
                if (DS.Tables[0].Rows.Count != 0 )
                {
                    TextName.Text = DS.Tables[0].Rows[0][1].ToString();
                    TextSurname.Text = DS.Tables[0].Rows[0][2].ToString();
                    TextEmail.Text = DS.Tables[0].Rows[0][3].ToString();
                    TextContact.Text = DS.Tables[0].Rows[0][4].ToString();
                }
                else
                {
                    TextName.Text = "";
                    TextSurname.Text = "";
                    TextEmail.Text = "";
                    TextContact.Text = "";
                    MessageBox.Show("Nie znaleziono pasującego PESELu");
                }

            }
        }
        private void ButtonAdd(object sender, RoutedEventArgs e)
        {
            if (TextName.Text != "")
            {

                String PESEL2 = TextPesel.Text;
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = con;

                cmd2.CommandText = "select ID from Users where PESEL ='" + PESEL2 + "'";
                SqlDataAdapter sda = new SqlDataAdapter(cmd2);
                DataSet DS = new DataSet();
                sda.Fill(DS);

                Int64 UserID= Int64.Parse(DS.Tables[0].Rows[0][0].ToString());
                

                if (Books.SelectedIndex != -1)
                {
                    String PESEL = TextPesel.Text;
                    String Name = TextName.Text;
                    String Surname = TextSurname.Text;
                    String Email = TextEmail.Text;
                    Int64 Contact = Int64.Parse(TextContact.Text);
                    String BookName = Books.Text;
                    String BookIssueDate = Date.Text;


                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = "insert into Operacje (PESEL, UserID, Name, Surname, Email, Contact, Bookname, Issuedate) values ('" + PESEL + "', '"+UserID+"', '" + Name + "', '"+Surname+"', '"+Email+"', '"+Contact+"', '"+BookName+"', '"+ BookIssueDate + "')";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Pomyślnie wypożyczono ksiażkę!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Błąd!. Wybierz książkę!");
                }
            }
            else
            {
                MessageBox.Show("Proszę wpisać poprawny PESEL!");
            }
        }

        private void TextPesel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextPesel.Text== "")
            {
                TextName.Text = "";
                TextSurname.Text = "";
                TextEmail.Text = "";
                TextContact.Text = "";
            }
        }
    }
}
