using System.Data;
using System.Windows;

namespace Shop_App_Sql_WPF
{
    /// <summary>
    /// Логика взаимодействия для AllView.xaml
    /// </summary>
    public partial class AllView : Window
    {
        private Repository _repository = new();
        DataTable dt;

        public AllView()
        {
            InitializeComponent();
            LoadPurchasesDataToAllView();
        }

        private void LoadPurchasesDataToAllView()
        {
            dt = new();
            dt.Clear();
            _repository.LoadPurchasesData(dt);
            gridAllView.Items.Refresh();
            gridAllView.ItemsSource = dt.DefaultView;
        }
    }
}
