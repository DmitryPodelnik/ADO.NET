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
    class LecturerViewModel : INotifyPropertyChanged
    {
        private Lecturer _selectedLecturer;
        public ObservableCollection<Lecturer> Lecturers { get; set; }
        public Lecturer SelectedLecturer
        {
            get { return _selectedLecturer; }
            set
            {
                _selectedLecturer = value;
                OnPropertyChanged("SelectedLecturer");
            }
        }

        public LecturerViewModel()
        {
            Lecturers = new();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
