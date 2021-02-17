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
using System.Data.SqlClient;
using System.Data;

namespace WpfApp5
{

    public partial class BooksPage : Page
    {

        SqlConnection con = new SqlConnection(@"Data Source=(local);Initial Catalog=Library;Integrated Security=True");
        DBAccess objDBAccess = new DBAccess();
        DataTable dtBooks = new DataTable();
        public BooksPage()
        {
            InitializeComponent();
            Refresh();
        }

        private void Refresh()
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("select ID,Name,Author,Publisher,Ammount from books", con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter dataAdp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("books");
            dataAdp.Fill(dt);
            DataGridView1.ItemsSource = dt.DefaultView;
            dataAdp.Update(dt);

            con.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
           
            SqlCommand cmd = new SqlCommand("select ID,Name,Author,Publisher,Ammount from books", con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter dataAdp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("books");
            dataAdp.Fill(dt);
            DataGridView1.ItemsSource = dt.DefaultView;
            dataAdp.Update(dt);

            con.Close();
            
        }

        private void ButtonAdd(object sender, RoutedEventArgs e)
        {
            AddBook dodaj = new AddBook();
            dodaj.Show();
        }
      

        private void SearchID(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Books where ID='" + SearchBoxID.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            DataGridView1.ItemsSource = dt.DefaultView;
            da.Update(dt);


            con.Close();

        }

        private void SearchName(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Books where Name LIKE'" + SearchBoxName.Text + "%'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            DataGridView1.ItemsSource = dt.DefaultView;
            da.Update(dt);


            con.Close();
        }

        private void SearchAuthor(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Books where Author LIKE'" + SearchBoxAuthor.Text + "%'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            DataGridView1.ItemsSource = dt.DefaultView;
            da.Update(dt);


            con.Close();
        }
        Int64 rowid;
        private void DataGridView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            
            if (row_selected != null)
            {
                rowid = Int64.Parse(row_selected["ID"].ToString());
                TextName.Text = row_selected["Name"].ToString();
                TextAuthor.Text = row_selected["Author"].ToString();
                TextPublisher.Text = row_selected["Publisher"].ToString();
                TextAmmount.Text = row_selected["Ammount"].ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz anulować akcję?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                TextName.Text = "";
                TextAuthor.Text = "";
                TextPublisher.Text = "";
                TextAmmount.Text = "";
            }

        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz zaktualizować książkę?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                string BookName = TextName.Text;
                string Author = TextAuthor.Text;
                string Publisher = TextPublisher.Text;
                Int64 Ammount = Int64.Parse(TextAmmount.Text);

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Books set Name = '" + BookName + "', Author = '" + Author + "', Publisher = '" + Publisher + "', Ammount = '" + Ammount + "' where ID=" + rowid + "";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Close();
                Refresh();
                TextName.Text = "";
                TextAuthor.Text = "";
                TextPublisher.Text = "";
                TextAmmount.Text = "";
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz usunąć książkę?", "Potwierdzenie", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                TextName.Text = "";
                TextAuthor.Text = "";
                TextPublisher.Text = "";
                TextAmmount.Text = "";
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from Books where ID=" + rowid + "";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Close();
                Refresh();
            }

        }
    }
    }

