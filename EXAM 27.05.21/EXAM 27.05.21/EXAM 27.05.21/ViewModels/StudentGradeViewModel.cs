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
using Microsoft.EntityFrameworkCore;

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
                    if (_window.Title == "Addition")
                        AddStudentGrade(Int32.Parse(_window.textStudent.Text), _window.textMark.Text, Int16.Parse(_window.textGrade.Text), _window.textSubject.Text);
                    else
                        EditStudentGrade();
                }));
            }
        }
        public StudentGradeViewModel(StudentGradeEdition window)
        {
            _window = window;

            if (_mainWindow.mainDataGrid.SelectedItem != null)
            {
                string fullString = _mainWindow.mainDataGrid.SelectedItem.ToString();

                _window.textStudent.Text = fullString.Substring(fullString.IndexOf("Learner = ") + 10, fullString.Substring(fullString.IndexOf("Learner = ") + 10).IndexOf(","));
                _window.textMark.Text = fullString.Substring(fullString.IndexOf("/") + 1, fullString.Substring(fullString.IndexOf("/") + 1).IndexOf(","));
                _window.textGrade.Text = fullString.Substring(fullString.IndexOf("Evaluation = ") + 13, fullString.Substring(fullString.IndexOf("Evaluation = ") + 13).IndexOf("/"));
                _window.textSubject.Text = fullString.Substring(fullString.IndexOf("Record = ") + 9, (fullString.Substring(fullString.LastIndexOf("= ")).IndexOf("}") - 3));
            }
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

        public async Task EditStudentGrade()
        {
            int i = _mainWindow.mainDataGrid.SelectedIndex;
            string stringItem = _mainWindow.mainDataGrid.Items[i].ToString();  // this give you access to the row
            string stringId = null;

            stringId = stringItem.Substring(7, 1);

            int id = Int32.Parse(stringId);

            string firstNameStudent = _window.textStudent.Text.Substring(0, _window.textStudent.Text.IndexOf(" "));
            string lastNameStudent = _window.textStudent.Text.Substring(_window.textStudent.Text.IndexOf(" ") + 1);

            var student = await StepAcademyDataBase.Context.Students.FirstOrDefaultAsync(a => a.FirstName + " " + a.LastName ==
                                                                             firstNameStudent + " " + lastNameStudent);
            if (student == null)
            {
                MessageBox.Show("You entered incorrect student!", "Error");
                return;
            }

            var editStudentGrade = await StepAcademyDataBase.Context.StudentGrades.FirstOrDefaultAsync(a => a.Id == id);
            if (editStudentGrade != null)
            {
                editStudentGrade.StudentId = student.Id;
                
                //StepAcademyDataBase.Context.Entry(editStudentGrade).State = EntityState.Modified;
                StepAcademyDataBase.Context.Update(editStudentGrade);
            }
            await StepAcademyDataBase.Context.SaveChangesAsync();

            var record = await StepAcademyDataBase.Context.Records.FirstOrDefaultAsync(a => a.Subject == _window.textSubject.Text);
            if (record == null)
            {
                MessageBox.Show("You entered incorrect subject!", "Error");
                return;
            }

            var editGrade = await StepAcademyDataBase.Context.Grades.FirstOrDefaultAsync(a => a.StudentGradeId == editStudentGrade.Id);
            if (editGrade != null)
            {
                editGrade.Value = Int16.Parse(_window.textGrade.Text);
                editGrade.Mark = _window.textMark.Text;
                editGrade.RecordId = record.Id;
            }
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("Student's grade has been successfully edited!");
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

            MessageBox.Show("A new student's grade has been successfully added!");

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
