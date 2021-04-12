using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Linq;

namespace LINQ_Second_12._04._21
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Stone> _stones = new ObservableCollection<Stone>();
        ObservableCollection<string> _stonesName = new ObservableCollection<string>();
        ObservableCollection<string> _stoneTypes = new ObservableCollection<string>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void stoneType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _stones.Clear();
            _stonesName.Clear();

            XDocument xdoc = XDocument.Load("../../../stones.xml");

            //var items = from xe in xdoc.Element("stones").Elements("stone")
            //            where xe.Element("type").Value == (string)stoneType.SelectedItem
            //            select new Stone
            //            {
            //                Transparency = Convert.ToBoolean(xe.Attribute("transparency").Value),
            //                Name = xe.Element("name").Value.ToString(),
            //                Type = xe.Element("type").Value,
            //                Color = xe.Element("color").Value,
            //                Description = xe.Element("description").Value
            //            };

            var items = xdoc.Element("stones").Elements("stone")
                        .Where(xe => xe.Element("type").Value == (string)stoneType.SelectedItem)
                        .Select(xe => new Stone 
                        {
                            Transparency = Convert.ToBoolean(xe.Attribute("transparency").Value),
                            Name = xe.Element("name").Value.ToString(),
                            Type = xe.Element("type").Value,
                            Color = xe.Element("color").Value,
                            Description = xe.Element("description").Value
                        });
            foreach (var item in items)
            {
                _stones.Add(item);
                _stonesName.Add("Name: " + item.Name + " Type: " + item.Type + " Color: " + item.Color + " Transparency: " + item.Transparency);
            }
            mainListView.ItemsSource = _stonesName;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            XDocument xdoc = XDocument.Load("../../../stones.xml");
            foreach (XElement stoneElement in xdoc.Element("stones").Elements("stone"))
            {
                XElement typeElement = stoneElement.Element("type");

                if (typeElement != null)
                {
                    _stoneTypes.Add(typeElement.Value);
                }
            }
            stoneType.ItemsSource = _stoneTypes;
        }
    }
}
