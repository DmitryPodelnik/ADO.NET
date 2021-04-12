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

namespace LINQ_Third_12._04._21
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Controller _controller;

        public Controller Controller
        {
            get => _controller;
        }
        public MainWindow()
        {
            InitializeComponent();
            _controller = new Controller(ref mainList);
        }

        private void readKey_Click(object sender, RoutedEventArgs e)
        {
            _controller.ReadFile();
            readKey.IsEnabled = false;
        }

        private void saveKey_Click(object sender, RoutedEventArgs e)
        {
            _controller.SaveFile();
        }

        private void showKey_Clicked(object sender, RoutedEventArgs e)
        {
            _controller.ShowPhones();
        }

        private void mainListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _controller.EditPhone();
        }

        private void numberSort_Clicked(object sender, RoutedEventArgs e)
        {
            _controller.SortByPhone();
        }

        private void nameSort_Clicked(object sender, RoutedEventArgs e)
        {
            _controller.SortByName();
        }

        private void addKey_Clicked(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow();
            editWindow.isAddition = true;
            editWindow.ShowDialog();
            editWindow.Title = "Number addition";
        }

        private void deleteKey_Clicked(object sender, RoutedEventArgs e)
        {
            Confirmation confirmationWindow = new Confirmation();
            confirmationWindow.ShowDialog();
            if (confirmationWindow.DialogResult == true)
            {
                _controller.DeletePhone(mainList.SelectedItem.ToString());
                _controller.ShowPhones();
            }
        }
    }
}
