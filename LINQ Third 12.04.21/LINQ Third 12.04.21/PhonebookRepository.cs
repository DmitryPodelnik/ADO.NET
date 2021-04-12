using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LINQ_Third_12._04._21
{
    [Serializable]
    public class PhonebookRepository
    {
        private ObservableCollection<Number> _phones = new ObservableCollection<Number>();

        public ObservableCollection<Number> Phones
        {
            get => _phones;
        }

        public PhonebookRepository() { }
    }
}
