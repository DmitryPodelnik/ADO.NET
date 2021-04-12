using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LINQ_Third_12._04._21
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
        private string _currentNumber;
        public bool isAddition { get; set; } = false;
        public EditWindow()
        {
            InitializeComponent();
        }

        private void EditWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isAddition)
            {
                nameTextBox.Text = _mainWindow.mainList.SelectedItem.ToString().Substring(6, _mainWindow.mainList.SelectedItem.ToString().IndexOf(",") - 6);
                numberTextBox.Text = _mainWindow.mainList.SelectedItem.ToString().Substring(_mainWindow.mainList.SelectedItem.ToString().LastIndexOf(":") + 2, _mainWindow.mainList.SelectedItem.ToString().Length - _mainWindow.mainList.SelectedItem.ToString().LastIndexOf(":") - 2);
                _currentNumber = numberTextBox.Text;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Title == "Number addition")
            {
                _mainWindow.Controller.AddPhone(nameTextBox.Text, numberTextBox.Text);
            }
            else
            {
                foreach (var item in _mainWindow.Controller.Repository.Phones)
                {
                    if (item.Phone == _currentNumber)
                    {
                        item.Phone = numberTextBox.Text;
                        item.Name = nameTextBox.Text;
                    }
                }
            }

            this.Close();
            _mainWindow.Controller.ShowPhones();
        }
    }
}
