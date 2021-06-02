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
    class AcademyPhoneViewModel : INotifyPropertyChanged
    {
        private AcademyPhone _selectedAcademyPhone;
        public ObservableCollection<AcademyPhone> AcademyPhones { get; set; }
        public AcademyPhone SelectedAcademyPhone
        {
            get { return _selectedAcademyPhone; }
            set
            {
                _selectedAcademyPhone = value;
                OnPropertyChanged("SelectedAcademyPhone");
            }
        }

        public AcademyPhoneViewModel()
        {
            AcademyPhones = new();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
