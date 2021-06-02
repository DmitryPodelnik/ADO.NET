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
    class StudentGradeViewModel : INotifyPropertyChanged
    {
        private StudentGrade _selectedStudentGrade;
        public ObservableCollection<StudentGrade> StudentGrades { get; set; }
        public StudentGrade SelectedStudentGrade
        {
            get { return _selectedStudentGrade; }
            set
            {
                _selectedStudentGrade = value;
                OnPropertyChanged("SelectedStudentGrade");
            }
        }

        public StudentGradeViewModel()
        {
            StudentGrades = new();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
