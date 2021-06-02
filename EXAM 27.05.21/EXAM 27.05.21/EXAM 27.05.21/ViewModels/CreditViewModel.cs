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
    class CreditViewModel : INotifyPropertyChanged
    {
        private Credit _selectedCredit;
        public ObservableCollection<Credit> Credits { get; set; }
        public Credit SelectedCredit
        {
            get { return _selectedCredit; }
            set
            {
                _selectedCredit = value;
                OnPropertyChanged("SelectedCredit");
            }
        }

        public CreditViewModel()
        {
            Credits = new();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
