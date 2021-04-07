using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
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

namespace Connected_Layer_04._04._21
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<string> _result = new List<string>();
        private List<string> _tableNames = new List<string>();
        // Если во внешнем config-файле MyLibrary.exe.config указана строка соединения,
        // берем ее оттуда. Если не указана, то берем ее из вкомпилированного App.config.
        // Если и там ее нет, то берем дефолтную строку соединения. (На самом деле она там
        // есть, смотри App.config в exe-проектах. MyLibrary.exe.config создается при
        // компиляции автоматически на основе App.config.)

        // Дефолтная строка соединения
        static private string _defaultConnectionString = "Data Source=(local);Initial Catalog=Sales;Integrated Security=True";

        // Классы ConfigurationManager и ConnectionStringSettings требуют
        // явного подключения сборки System.Configuration
        static private ConnectionStringSettings _settings = ConfigurationManager.ConnectionStrings["Sales"];
        private string _connectionString = _settings != null ? _settings.ConnectionString : _defaultConnectionString;
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ApplicationViewModel();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    // Открываем соединение
                    connection.Open();

                    DataTable schema = connection.GetSchema("Tables");
                    foreach (DataRow row in schema.Rows)
                    {
                        _tableNames.Add(row[2].ToString());
                    }
                    tableNames.ItemsSource = _tableNames;

                } // Закрываем соединение
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TableNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                mainTable.View = null;
                mainTable.Items.Clear();

                GridView myGridView = new GridView();
                myGridView.AllowsColumnReorder = true;
                myGridView.ColumnHeaderToolTip = "Sales Information";

                using (var connection = new SqlConnection(_connectionString))
                {
                    // Открываем соединение
                    connection.Open();

                    ///Запрос
                    string queryAllBooks = $"SELECT * FROM {tableNames.SelectedItem}";

                    // Отправляем запрос и пробегаемся по полученной выборке
                    using (var command = new SqlCommand(queryAllBooks, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        GridViewColumn gvc1 = new GridViewColumn();
                        gvc1.Header = reader.GetName(0).Replace(" ", "");
                        gvc1.Width = 100;
                        GridViewColumn gvc2 = new GridViewColumn();
                        gvc2.Header = reader.GetName(1).Replace(" ", "");
                        gvc2.Width = 100;
                        GridViewColumn gvc3 = new GridViewColumn();
                        gvc3.Header = reader.GetName(2).Replace(" ", "");
                        gvc3.Width = 100;

                        gvc1.DisplayMemberBinding = new Binding(reader.GetName(0).Replace(" ", ""));
                        gvc2.DisplayMemberBinding = new Binding(reader.GetName(1).Replace(" ", ""));
                        gvc3.DisplayMemberBinding = new Binding(reader.GetName(2).Replace(" ", ""));

                        myGridView.Columns.Add(gvc1);
                        myGridView.Columns.Add(gvc2);
                        myGridView.Columns.Add(gvc3);

                        while (reader.Read())
                        {
                            if ((string)tableNames.SelectedItem != "Sales")
                            {
                                mainTable.Items.Add(new { Id = reader.GetValue(0), FirstName = reader.GetValue(1), LastName = reader.GetValue(2) });
                            }
                            else
                            {
                                mainTable.Items.Add(new { Id = reader.GetValue(0), CustomId = reader.GetValue(1), SalespersonId = reader.GetValue(2) });
                            }

                        }
                        mainTable.View = myGridView;
                    }
                } // Закрываем соединение
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
