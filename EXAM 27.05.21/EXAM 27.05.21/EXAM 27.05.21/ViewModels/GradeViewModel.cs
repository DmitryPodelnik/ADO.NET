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
    class GradeViewModel : INotifyPropertyChanged
    {
        private Grade _selectedGrade;
        public ObservableCollection<Grade> Grades { get; set; }
        public Grade SelectedGender
        {
            get { return _selectedGrade; }
            set
            {
                _selectedGrade = value;
                OnPropertyChanged("SelectedGrade");
            }
        }

        public GradeViewModel()
        {
            Grades = new();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
