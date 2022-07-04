using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Shop_App_Sql_WPF
{
    /// <summary>
    /// Логика взаимодействия для Access.xaml
    /// </summary>
    public partial class Access : Window
    {
        public Access()
        {
            InitializeComponent();
        }

        private SqlConnection con = new SqlConnection(
            @"Data Source = (localdb)\MSSQLLocalDB; 
              Initial Catalog = ShopDB; 
              Integrated Security = True;");

        
        private void btnGetAccess(object sender, RoutedEventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                String query = "SELECT COUNT(1) FROM Access WHERE Username=@Username AND Password=@Password";
                SqlCommand cmd = new (query, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Password);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 1)
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username or password is incorrect.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
