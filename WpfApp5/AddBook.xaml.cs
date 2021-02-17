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
    /// Interaction logic for AddBook.xaml
    /// </summary>
    public partial class AddBook : Window
    {
        SqlConnection con = new SqlConnection(@"Data Source=(local);Initial Catalog=Library;Integrated Security=True");
        DBAccess objDBAccess = new DBAccess();
        DataTable dtBooks = new DataTable();
        public AddBook()
        {
            InitializeComponent();
        }

        private void ButtonAdd(object sender, RoutedEventArgs e)
        {
            string Name = BookName.Text;
            string Author = BookAuthor.Text;
            string Publisher = BookPublisher.Text;
            string Ammount = BookAmmount.Text;
            con.Open();
            SqlCommand cmd;
            string sql = "SELECT count(ID) FROM Books";
            cmd = new SqlCommand(sql, con);
            Int32 rows_count = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            int ID = rows_count + 1;

            if (Name.Equals(""))
            {
                MessageBox.Show("Wprowadź nazwę książki!");
            }
            else if (Author.Equals(""))
            {
                MessageBox.Show("Wprowadź autora książki!");
            }
            else if (Publisher.Equals(""))
            {
                MessageBox.Show("Wprowadź wydawnictwo!");
            }
            else if (Ammount.Equals(""))
            {
                MessageBox.Show("Wprowadź ilość!");
            }
            else
            {
                SqlCommand insertCommand = new SqlCommand("insert into Books(Id,Name,Author,Publisher,Ammount) values(@ID, @Name, @Author,@Publisher,@Ammount)");

                insertCommand.Parameters.AddWithValue("@ID", ID);
                insertCommand.Parameters.AddWithValue("@Name", Name);
                insertCommand.Parameters.AddWithValue("@Author", Author);
                insertCommand.Parameters.AddWithValue("@Publisher", Publisher);
                insertCommand.Parameters.AddWithValue("@Ammount", Ammount);

                int row = objDBAccess.executeQuery(insertCommand);

                if (row == 1)
                {
                    MessageBox.Show("Dodano książkę!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("BŁĄD!");
                }
            }
        }

        private void ButtonCancel(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz anulować akcję?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
