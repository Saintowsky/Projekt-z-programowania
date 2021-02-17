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
    /// Interaction logic for ReturnBook.xaml
    /// </summary>
    public partial class ReturnBook : Window
    {
        SqlConnection con = new SqlConnection(@"Data Source=(local);Initial Catalog=Library;Integrated Security=True");
        DBAccess objDBAccess = new DBAccess();
        DataTable dtBooks = new DataTable();
        public ReturnBook()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string PESEL = TextPesel.Text;
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Operacje where PESEL= '"+PESEL+"' AND ReturnDate IS NULL";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count !=0)
            {
              IssuesGrid.ItemsSource = dt.DefaultView;
              da.Update(dt);
            }
            else
            {
                MessageBox.Show("Błędny pesel, lub użytkownik nie wypożyczył żadnej książki");
            }
            con.Close();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "update Operacje set ReturnDate = '" + ReturnDate.Text + "' where PESEL = '" + TextPesel.Text + "' and TransactionID = '"+rowid+"'";
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Pomyślnie zwrócono książkę!");
        }

        Int64 rowid;

        private void IssuesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                rowid = Int64.Parse(row_selected["TransactionID"].ToString());
                TextName.Text = row_selected["BookName"].ToString();
                TestIssueDate.Text = row_selected["IssueDate"].ToString();
            }
        }
    }
}
