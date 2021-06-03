using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EXAM_27._05._21.Models;
using EXAM_27._05._21.ViewModels;

namespace EXAM_27._05._21
{
    class AcademyViewModel : INotifyPropertyChanged
    {
        private Academy _selectedAcademy;
        public ObservableCollection<Academy> Academies { get; set; }
        public Academy SelectedAcademy
        {
            get { return _selectedAcademy; }
            set
            {
                _selectedAcademy = value;
                OnPropertyChanged("SelectedAcademy");
            }
        }

        public AcademyViewModel()
        {
            Academies = new();
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
