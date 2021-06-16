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
    class StudentViewModel : INotifyPropertyChanged
    {
        private StepAcademy _mainWindow = (StepAcademy)Application.Current.MainWindow;
        private StudentEdition _window;

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand =
                (_saveCommand = new RelayCommand(obj =>
                {
                    AddStudent(_window.textFirstName.Text, _window.textLastName.Text, Convert.ToDateTime(_window.textBirthDate.Text),
                               _window.textGradeBookNumber.Text, _window.textNote.Text, _window.textPhone.Text,
                               _window.textEmail.Text, Int16.Parse(_window.textYear.Text), Int32.Parse(_window.textGroup.Text),
                               Int32.Parse(_window.textGender.Text), Int32.Parse(_window.textAddress.Text));
                }));
            }
        }
        public StudentViewModel(StudentEdition window)
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

        public async Task AddStudent(string firstName, string lastName, DateTime birthDate, string gradeBookNumber, 
                                     string note, string phone, string email, short admissionYear, 
                                     int groupId, int genderId, int addressId)
        {
            var newStudent = new Student
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate,
                GradeBookNumber = gradeBookNumber,
                Note = note,
                Phone = phone,
                Email = email,
                AdmissionYear = admissionYear,
                GroupId = groupId,
                GenderId = genderId,
                AddressId = addressId
            };
            await StepAcademyDataBase.Context.Students.AddAsync(newStudent);
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("A new student has been successfully added!");

            ClearTextBoxes();

            void ClearTextBoxes()
            {
                _window.textFirstName.Text = "";
                _window.textLastName.Text = "";
                _window.textBirthDate.Text = "";
                _window.textGradeBookNumber.Text = "";
                _window.textNote.Text = "";
                _window.textPhone.Text = "";
                _window.textEmail.Text = "";
                _window.textYear.Text = "";
                _window.textGroup.Text = "";
                _window.textGender.Text = "";
                _window.textAddress.Text = "";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
