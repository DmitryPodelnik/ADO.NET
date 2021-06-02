using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EXAM_27._05._21.Models;

namespace EXAM_27._05._21.ViewModels
{
    class AddressViewModel : INotifyPropertyChanged
    {
        private Address _selectedAddress;
        public ObservableCollection<Address> Addresses { get; set; }
        public Address SelectedAddress
        {
            get { return _selectedAddress; }
            set
            {
                _selectedAddress = value;
                OnPropertyChanged("SelectedAddress");
            }
        }

        public AddressViewModel()
        {
            Addresses = new();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
