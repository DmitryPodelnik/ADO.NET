using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EXAM_27._05._21.Models;
using EXAM_27._05._21.Views;

namespace EXAM_27._05._21.ViewModels
{
    class StudentGradeViewModel : INotifyPropertyChanged
    {
        private StepAcademy _mainWindow = (StepAcademy)Application.Current.MainWindow;
        private StudentGradeEdition _window;

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand =
                (_saveCommand = new RelayCommand(obj =>
                {
                    AddStudentGrade(Int32.Parse(_window.textStudent.Text), _window.textMark.Text, Int16.Parse(_window.textGrade.Text), _window.textSubject.Text);
                }));
            }
        }
        public StudentGradeViewModel(StudentGradeEdition window)
        {
            _window = window;
        }

        private RelayCommand _closeWindow;
        public RelayCommand CloseWindow
        {
            get
            {
                return _closeWindow =
                (_closeWindow = new RelayCommand(obj =>
                {
                    _window.Close();
                }));
            }
        }

        public async Task AddStudentGrade(int studentId, string mark, short value, string subject)
        {
            var newStudentGrade = new StudentGrade
            {
                StudentId = studentId
            };
            await StepAcademyDataBase.Context.StudentGrades.AddAsync(newStudentGrade);
            var sg = StepAcademyDataBase.Context.StudentGrades.FirstOrDefault(a => a.StudentId == studentId);
            var subjc = StepAcademyDataBase.Context.Records.FirstOrDefault(s => s.Subject == subject);
            if (subjc == null)
            {
                var newRecord = new Record
                {
                    Subject = subject,
                    Coins = 0,
                    Course = 0,
                    StudentGradeId = studentId
                };
                await StepAcademyDataBase.Context.Records.AddAsync(newRecord);
                await StepAcademyDataBase.Context.SaveChangesAsync();
                subjc = StepAcademyDataBase.Context.Records.FirstOrDefault(s => s.Subject == newRecord.Subject);
            }

            var newGrade = new Grade
            {
                Mark = mark,
                Value = value,
                StudentGradeId = sg.Id,
                RecordId = subjc.Id    
            };

            await StepAcademyDataBase.Context.Grades.AddAsync(newGrade);
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("A new student grade has been successfully added!");

            ClearTextBoxes();

            void ClearTextBoxes()
            {
                _window.textStudent.Text = "";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
