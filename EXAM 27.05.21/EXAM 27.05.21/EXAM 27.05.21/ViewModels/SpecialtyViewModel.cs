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
    class SpecialtyViewModel : INotifyPropertyChanged
    {
        private Specialty _selectedSpecialty;
        public ObservableCollection<Specialty> Specialties { get; set; }
        public Specialty SelectedSpecialty
        {
            get { return _selectedSpecialty; }
            set
            {
                _selectedSpecialty = value;
                OnPropertyChanged("SelectedSpecialty");
            }
        }

        public SpecialtyViewModel()
        {
            Specialties = new();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
