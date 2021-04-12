using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace LINQ_Third_12._04._21
{
    public class Controller
    {
        private PhonebookRepository _repository = new PhonebookRepository();
        private ListView _mainListView;

        public PhonebookRepository Repository
        {
            get => _repository;
        }

        public Controller(ref ListView listView)
        {
            _mainListView = listView;
        }

        public void ReadFile()
        {
            _repository.Phones.Clear();
            XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<Number>));
            //XDocument xdoc = XDocument.Load("../../../phones.xml");

            //var items = xdoc.Element("ArrayOfNumber").Elements("Number")
            //           .Select(xe => new Number
            //           {
            //               Name = xe.Element("Name").Value,
            //               Phone = xe.Element("Number").Value
            //           });
            using (FileStream fs = new FileStream("../../../phones.xml", FileMode.OpenOrCreate))
            {
                ObservableCollection<Number> items = (ObservableCollection<Number>)formatter.Deserialize(fs);

                foreach (var item in items)
                {
                    _repository.Phones.Add(item);
                }
            }
        }

        public void SaveFile()
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("../../../phones.xml");

                // получаем корневой элемент
                XmlElement xRoot = xDoc.DocumentElement;

                xRoot.RemoveAll();
                xDoc.Save("../../../phones.xml");

                XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<Number>));

                using (FileStream fs = new FileStream("../../../phones.xml", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, _repository.Phones);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ShowPhones()
        {
            ObservableCollection<string> _phones = new ObservableCollection<string>();
            foreach (var item in _repository.Phones)
            {
                _phones.Add("Name: " + item.Name + ", Phone: " + item.Phone);
            }
            _mainListView.ItemsSource = _phones;
        }

        public void SortByPhone()
        {
            var orderedNumbers = _repository.Phones
                       .OrderBy(p => p.Phone)
                       .Select(p => p);

            ObservableCollection<string> _phones = new ObservableCollection<string>();
            foreach (var item in orderedNumbers)
            {
                _phones.Add("Name: " + item.Name + ", Phone: " + item.Phone);
            }
            _mainListView.ItemsSource = _phones;
        }

        public void SortByName()
        {
            var orderedNumbers = _repository.Phones
                       .OrderBy(p => p.Name)
                       .Select(p => p);

            ObservableCollection<string> _phones = new ObservableCollection<string>();
            foreach (var item in orderedNumbers)
            {
                _phones.Add("Name: " + item.Name + ", Phone: " + item.Phone);
            }
            _mainListView.ItemsSource = _phones;
        }

        public void EditPhone()
        {
            EditWindow editwindow = new EditWindow();
            editwindow.ShowDialog();
        }

        public void AddPhone(string name, string phone)
        {
            Number number = new Number(name, phone);
            _repository.Phones.Add(number);
        }

        public void DeletePhone(string phone)
        {
            int i = 0;
            foreach (var item in _repository.Phones)
            {
                if (phone.Contains(item.Phone))
                {
                    _repository.Phones.RemoveAt(i);
                    break;
                }
                i++;
            }
        }
    }
}
