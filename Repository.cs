using System.Data;
using System.Data.SqlClient;

namespace Shop_App_Sql_WPF
{
    internal class Repository
    {

        private SqlConnection con = new SqlConnection(
            @"Data Source = (localdb)\MSSQLLocalDB; 
              Initial Catalog = ShopDB; 
              Integrated Security = True;");

        public void LoadClientData(DataTable dt)
        {
            SqlCommand cmd = new("Select * From Clients", con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
        }

        public void LoadElectronicsProducts(DataTable dt)
        {
            SqlCommand cmd = new("SELECT * From ElectronicsProducts", con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
        }

        public void LoadSpaceProducts(DataTable dt)
        {
            SqlCommand cmd = new("SELECT * From SpaceProducts", con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
        }

        public void LoadFoodsProducts(DataTable dt)
        {
            SqlCommand cmd = new("SELECT * From FoodsProducts", con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
        }

        public void LoadPurchasesData(DataTable dt)
        {
            SqlCommand cmd = new("Select * From Purchases", con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
        }
    }
}
