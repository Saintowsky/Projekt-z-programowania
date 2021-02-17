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
    /// <summary>
    /// Interaction logic for UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=(local);Initial Catalog=Library;Integrated Security=True");
        DBAccess objDBAccess = new DBAccess();
        DataTable dtBooks = new DataTable();

        public UsersPage()
        {
            InitializeComponent();
            Refresh();
        }
        private void Refresh()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Id, Name,Surname,Email, Contact, PESEL from Users";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            Datagrid.ItemsSource = dt.DefaultView;
            da.Update(dt);


            con.Close();
        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select ID,Name,Surname, Email, Contact, PESEL from Users where Name LIKE'" + SearchBoxName.Text + "%'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            Datagrid.ItemsSource = dt.DefaultView;
            da.Update(dt);


            con.Close();
        }

        private void SearchButtonID_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select ID,Name,Surname, Email, Contact, PESEL from Users where ID LIKE'" + SearchBoxID.Text + "%'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            Datagrid.ItemsSource = dt.DefaultView;
            da.Update(dt);


            con.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddUser Dodaj = new AddUser();
            Dodaj.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        Int64 rowid;
        private void Datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                rowid = Int64.Parse(row_selected["ID"].ToString());
                TextName.Text = row_selected["Name"].ToString();
                TextSurname.Text = row_selected["Surname"].ToString();
                TextEmail.Text = row_selected["Email"].ToString();
                TextContact.Text = row_selected["Contact"].ToString();
                TextPesel.Text = row_selected["PESEL"].ToString();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            TextName.Text = "";
            TextSurname.Text = "";
            TextEmail.Text = "";
            TextContact.Text = "";
            TextPesel.Text = "";
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz usunąć użytkownika?", "Potwierdzenie", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                TextName.Text = "";
                TextSurname.Text = "";
                TextEmail.Text = "";
                TextContact.Text = "";
                TextPesel.Text = "";
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from Users where ID=" + rowid + "";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Close();
                Refresh();
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz zaktualizować użytkownika?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                string Name = TextName.Text;
                string Surname = TextSurname.Text;
                string Email = TextEmail.Text;
                string Contact = TextContact.Text;
                string PESEL = TextPesel.Text;

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Users set Name = '" + Name + "', Surname = '" + Surname + "', Email = '" + Email + "', Contact = '" + Contact + "', PESEL = '"+PESEL+"' where ID=" + rowid + "";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Close();
                Refresh();
                TextName.Text = "";
                TextSurname.Text = "";
                TextEmail.Text = "";
                TextContact.Text = "";
                TextPesel.Text = "";
            }
        }
    }
}
