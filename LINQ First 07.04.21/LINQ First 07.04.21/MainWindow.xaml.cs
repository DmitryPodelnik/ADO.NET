using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq;
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
using System.Collections.ObjectModel;

namespace LINQ_First_07._04._21
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<string> _result = new ObservableCollection<string>();
        private ObservableCollection<string> _lastNames = new ObservableCollection<string>();
        // Если во внешнем config-файле MyLibrary.exe.config указана строка соединения,
        // берем ее оттуда. Если не указана, то берем ее из вкомпилированного App.config.
        // Если и там ее нет, то берем дефолтную строку соединения. (На самом деле она там
        // есть, смотри App.config в exe-проектах. MyLibrary.exe.config создается при
        // компиляции автоматически на основе App.config.)

        // Дефолтная строка соединения
        static private string _defaultConnectionString = @"Data Source=(local);Initial Catalog=Sales;Integrated Security=True";

        // Классы ConfigurationManager и ConnectionStringSettings требуют
        // явного подключения сборки System.Configuration
        static private ConnectionStringSettings _settings = ConfigurationManager.ConnectionStrings["Sales"];
        private string _connectionString = _settings != null ? _settings.ConnectionString : _defaultConnectionString;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string sql = "SELECT [Last Name] AS LastName " +
                             "FROM Salespeople";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    // Создаем объект DataAdapter
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    // Создаем объект Dataset
                    DataSet ds = new DataSet();
                    // Заполняем Dataset
                    adapter.Fill(ds, "Salespeople");
                    // Отображаем данные
                    DataTable salespeople = ds.Tables["Salespeople"];
                    //var result = from salepeople in ds.Tables["Salespeople"].AsEnumerable()
                    //             select new
                    //             {
                    //                 lastName = salepeople["LastName"].ToString()
                    //             };
                    var result = salespeople.AsEnumerable()
                        .Select(p => new { LastName = p["LastName"].ToString() });

                    foreach (var item in result)
                    {
                        _lastNames.Add(item.LastName);
                    }
                    lastNames.ItemsSource = _lastNames;
                }
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lastNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                _result.Clear();

                string sql = "SELECT * " +
                             "FROM Schedule " +
                             $"WHERE LastName='{lastNames.SelectedItem}'";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    // Создаем объект DataAdapter
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    // Создаем объект Dataset
                    DataSet ds = new DataSet();
                    // Заполняем Dataset
                    adapter.Fill(ds, "Schedule");
                    // Отображаем данные
                    DataTable dates = ds.Tables["Schedule"];
                    //var result = from salepeople in ds.Tables["Schedule"].AsEnumerable()
                    //             select new
                    //             {
                    //                 lastName = salepeople["StartTime"].ToString()
                    //             };
                    var result = dates.AsEnumerable()
                        .Select(p => new { StartTime = p["StartTime"].ToString() });

                    foreach (var item in result)
                    {
                        _result.Add(item.StartTime);
                    }
                    mainListView.ItemsSource = _result;
                }
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
