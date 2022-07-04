using Saving_InfoLog_ClassLibrary;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Shop_App_Sql_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private InfoLog _log = new();
        private SavingMethod _savingInfoLogs = new();
        private Repository _repository = new();
        DataTable dt;
        private Access _access;

        public event Action<string> Transaction;

        private SqlConnection con = new SqlConnection(
            @"Data Source = (localdb)\MSSQLLocalDB; 
              Initial Catalog = ShopDB; 
              Integrated Security = True;");

        public MainWindow()
        {
            InitializeComponent();
            new Access().ShowDialog();
            Transaction += LogRepository_Transaction;
            LoadClientDataToCB();
            cSelectClient.Items.Refresh();
            infoList.ItemsSource = _log.log;
        }

        private void LogRepository_Transaction(string message)
        {
            _log.AddToLog(message);
            infoList.Items.Refresh();
            _savingInfoLogs.SaveInfoLog(_log.log);
        }

        private void LoadClientDataToCB()
        {
            dt = new();
            dt.Clear();
            _repository.LoadClientData(dt);
            cSelectClient.Items.Refresh();
            cSelectClient.ItemsSource = dt.DefaultView;
        }



        private void Button_Ok_AddClient(object sender, RoutedEventArgs e)
        {
            try
            {
                dt = new();
                dt.Clear();
                SqlCommand cmd = new("INSERT INTO Clients VALUES(@Name, @SurName, @Patronymic, @Phone, @E_Mail) SET @Id = @@IDENTITY;", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@Id", SqlDbType.Int, 4, "Id").Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@Name", Name_txt.Text);
                cmd.Parameters.AddWithValue("@SurName", SurName_txt.Text);
                cmd.Parameters.AddWithValue("@Patronymic", Patronymic_txt.Text);
                cmd.Parameters.AddWithValue("@Phone", Phone_txt.Text);
                cmd.Parameters.AddWithValue("@E_Mail", E_mail_txt.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                LoadClientDataToCB();
                Transaction?.Invoke($"Добавлен новый клиент {Name_txt.Text} {SurName_txt.Text} {Patronymic_txt.Text}");
                CleareTextBox();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Ok_DeleteClient(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new("Delete From Clients Where Id = " + Id_Delete_txt.Text + " ", con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                LoadClientDataToCB();
                Transaction?.Invoke($"Клиент {Id_Delete_txt.Text} удалён");
                CleareTextBox();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void cSelectClient_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {

        }

        private void cSelectedClient_Purchases(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_SelectElectronics_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dt = new();
                dt.Clear();
                _repository.LoadElectronicsProducts(dt);
                productsList.Items.Refresh();
                productsList.ItemsSource = dt.DefaultView;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_SelectSpaceP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dt = new();
                dt.Clear();
                _repository.LoadSpaceProducts(dt);
                productsList.Items.Refresh();
                productsList.ItemsSource = dt.DefaultView;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_SelectFood_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dt = new();
                dt.Clear();
                _repository.LoadFoodsProducts(dt);
                productsList.Items.Refresh();
                productsList.ItemsSource = dt.DefaultView;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProductsList_OnPreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                ContextMenu cm = this.FindResource("CmButton") as ContextMenu;
                cm.PlacementTarget = sender as Button;
                cm.IsOpen = true;
            }
        }

        private void MenuItem_Add_Client_Click(object sender, RoutedEventArgs e)
        {
            pAddClient.IsOpen = true;
        }

        private void MenuItem_Delete_Client_Click(object sender, RoutedEventArgs e)
        {
            pDeleteClient.IsOpen = true;
        }

        private void Button_Ok_DeleteElectronicProduct(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new("Delete From ElectronicsProducts Where ProductCode = " + DeleteProductElectronic_txt.Text + " ", con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Transaction?.Invoke($"Продукт {DeleteProductElectronic_txt.Text} удален");
                CleareTextBox();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void Button_Ok_AddElectronicProduct(object sender, RoutedEventArgs e)
        {
            try
            {
                dt = new();
                dt.Clear();
                SqlCommand cmd = new("INSERT INTO ElectronicsProducts VALUES(@ProductCode, @ProductName) SET @Id = @@IDENTITY;", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@Id", SqlDbType.Int, 4, "Id").Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@ProductCode", ElProductNumber_txt.Text);
                cmd.Parameters.AddWithValue("@ProductName", ElProductName_txt.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Transaction?.Invoke($"Добавлен новый продукт {ElProductNumber_txt.Text} {ElProductName_txt.Text}");
                CleareTextBox();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_BuyIt_Click(object sender, RoutedEventArgs e)
        {
            string shopN = ((DataRowView)productsList.SelectedItem).Row["ProductName"].ToString();
            string shopC = ((DataRowView)productsList.SelectedItem).Row["ProductCode"].ToString();
            string buyer = ((DataRowView)cSelectClient.SelectedItem).Row["E_Mail"].ToString();
            try
            {
                dt = new();
                dt.Clear();
                SqlCommand cmd = new("INSERT INTO Purchases VALUES(@E_Mail, @ProductCode, @ProductName) SET @Id = @@IDENTITY;", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@Id", SqlDbType.Int, 4, "Id").Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@E_Mail", buyer);
                cmd.Parameters.AddWithValue("@ProductCode", shopC);
                cmd.Parameters.AddWithValue("@ProductName", shopN);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Transaction?.Invoke($"Товар {shopN} куплен клиентом {buyer}");
                CleareTextBox();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void MenuItem_Add_Electronic_Product_Click(object sender, RoutedEventArgs e)
        {
            pAddElectronicProduct.IsOpen = true;
        }

        private void MenuItem_Delete_Electronic_Product_Click(object sender, RoutedEventArgs e)
        {
            pDeleteElectronicProduct.IsOpen = true;
        }

        private void ClientList_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ComboBox).SelectedItem;
            if (item != null)
            {
                ContextMenu cm = this.FindResource("CmButton") as ContextMenu;
                cm.PlacementTarget = sender as Button;
                cm.IsOpen = true;
            }
        }

        private void CleareTextBox()
        {
            try
            {
                Name_txt.Clear();
                SurName_txt.Clear();
                Patronymic_txt.Clear();
                Phone_txt.Clear();
                E_mail_txt.Clear();
                Id_Delete_txt.Clear();
                ElProductNumber_txt.Clear();
                ElProductName_txt.Clear();
                SpProductNumber_txt.Clear();
                SpProductName_txt.Clear();
                FoProductNumber_txt.Clear();
                FoProductName_txt.Clear();
                DeleteProductElectronic_txt.Clear();
                New_Phone_txt.Clear();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MenuItem_Buy_OnClick(object sender, RoutedEventArgs e)
        {
            pBuy.IsOpen = true;
        }

        private void Button_Ok_DeleteSpaceProduct(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new("Delete From SpaceProducts Where ProductCode = " + DeleteProductSpace_txt.Text + " ", con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Transaction?.Invoke($"Продукт {DeleteProductSpace_txt.Text} удален");
                CleareTextBox();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void MenuItem_Delete_Space_Product_Click(object sender, RoutedEventArgs e)
        {
            pDeleteSpaceProduct.IsOpen = true;
        }

        private void MenuItem_Delete_Food_Product_Click(object sender, RoutedEventArgs e)
        {
            pDeleteFoodProduct.IsOpen = true;
        }

        private void Button_Ok_DeleteFoodProduct(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new("Delete From FoodsProducts Where ProductCode = " + DeleteProductFood_txt.Text + " ", con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Transaction?.Invoke($"Продукт {DeleteProductFood_txt.Text} удален");
                CleareTextBox();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void MenuItem_Add_Space_Product_Click(object sender, RoutedEventArgs e)
        {
            pAddSpaceProduct.IsOpen = true;
        }

        private void MenuItem_Add_Food_Product_Click(object sender, RoutedEventArgs e)
        {
            pAddFoodProduct.IsOpen = true;
        }

        private void Button_Ok_AddSpaceProduct(object sender, RoutedEventArgs e)
        {
            try
            {
                dt = new();
                dt.Clear();
                SqlCommand cmd = new("INSERT INTO SpaceProducts VALUES(@ProductCode, @ProductName) SET @Id = @@IDENTITY;", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@Id", SqlDbType.Int, 4, "Id").Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@ProductCode", SpProductNumber_txt.Text);
                cmd.Parameters.AddWithValue("@ProductName", SpProductName_txt.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Transaction?.Invoke($"Добавлен новый продукт {SpProductNumber_txt.Text} {SpProductName_txt.Text}");
                CleareTextBox();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Ok_AddFoodProduct(object sender, RoutedEventArgs e)
        {
            try
            {
                dt = new();
                dt.Clear();
                SqlCommand cmd = new("INSERT INTO FoodsProducts VALUES(@ProductCode, @ProductName) SET @Id = @@IDENTITY;", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@Id", SqlDbType.Int, 4, "Id").Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@ProductCode", FoProductNumber_txt.Text);
                cmd.Parameters.AddWithValue("@ProductName", FoProductName_txt.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Transaction?.Invoke($"Добавлен новый продукт {FoProductNumber_txt.Text} {FoProductName_txt.Text}");
                CleareTextBox();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MenuItem_Purchases_OnClick(object sender, RoutedEventArgs e)
        {
            new AllView().ShowDialog();
        }

        private void MenuItem_Update_Client_Data_Click(object sender, RoutedEventArgs e)
        {
            pUpdateClient.IsOpen = true;
        }

        private void Button_Ok_UpdateClientData(object sender, RoutedEventArgs e)
        {
            string client = ((DataRowView)cSelectClient.SelectedItem).Row["Phone"].ToString();
            try
            {
                dt = new();
                dt.Clear();
                SqlCommand cmd = new("Update Clients " +
                                 "Set Phone = '" + New_Phone_txt.Text + "' " +
                                 $"Where Phone = '{client}'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                LoadClientDataToCB();
                Transaction?.Invoke($"№ телефона клиента {client} изменен на '{New_Phone_txt.Text}'");
                CleareTextBox();
            }
            catch (SqlException ex)
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
