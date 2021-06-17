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
                    if (_window.Title == "Addition")
                        AddStudent(_window.textFirstName.Text, _window.textLastName.Text, Convert.ToDateTime(_window.textBirthDate.Text),
                               _window.textGradeBookNumber.Text, _window.textNote.Text, _window.textPhone.Text,
                               _window.textEmail.Text, Int16.Parse(_window.textYear.Text), Int32.Parse(_window.textGroup.Text),
                               Int32.Parse(_window.textGender.Text), Int32.Parse(_window.textAddress.Text));
                    else
                        EditStudent();
                }));
            }
        }
        public StudentViewModel(StudentEdition window)
        {
            _window = window;

            if (_mainWindow.mainDataGrid.SelectedItem != null)
            {
                string fullString = _mainWindow.mainDataGrid.SelectedItem.ToString();

                _window.textFirstName.Text = fullString.Substring(fullString.IndexOf("FirstName = ") + 12, fullString.Substring(fullString.IndexOf("FirstName = ") + 12).IndexOf(","));
                _window.textLastName.Text = fullString.Substring(fullString.IndexOf("LastName = ") + 11, fullString.Substring(fullString.IndexOf("LastName = ") + 11).IndexOf(","));
                _window.textBirthDate.Text = fullString.Substring(fullString.IndexOf("BirthDate = ") + 12, fullString.Substring(fullString.IndexOf("BirthDate = ") + 12).IndexOf(","));
                _window.textGradeBookNumber.Text = fullString.Substring(fullString.IndexOf("GradeBookNumber = ") + 18, fullString.Substring(fullString.IndexOf("GradeBookNumber = ") + 18).IndexOf(","));
                _window.textNote.Text = fullString.Substring(fullString.IndexOf("Note = ") + 7, fullString.Substring(fullString.IndexOf("Note = ") + 7).IndexOf(","));
                _window.textPhone.Text = fullString.Substring(fullString.IndexOf("Phone = ") + 8, fullString.Substring(fullString.IndexOf("Phone = ") + 8).IndexOf(","));
                _window.textEmail.Text = fullString.Substring(fullString.IndexOf("Email = ") + 8, fullString.Substring(fullString.IndexOf("Email = ") + 8).IndexOf(","));
                _window.textYear.Text = fullString.Substring(fullString.IndexOf("AdmissionYear = ") + 16, fullString.Substring(fullString.IndexOf("AdmissionYear = ") + 16).IndexOf(","));
                _window.textGroup.Text = fullString.Substring(fullString.IndexOf("Class = ") + 8, fullString.Substring(fullString.IndexOf("Class = ") + 8).IndexOf(","));
                _window.textGender.Text = fullString.Substring(fullString.IndexOf("Sex = ") + 6, fullString.Substring(fullString.IndexOf("Sex = ") + 6).IndexOf(","));
                _window.textAddress.Text = fullString.Substring(fullString.IndexOf("Direction = ") + 12, (fullString.Substring(fullString.LastIndexOf("= ")).IndexOf("}") - 3));
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


        public async Task EditStudent()
        {
            int i = _mainWindow.mainDataGrid.SelectedIndex;
            string stringItem = _mainWindow.mainDataGrid.Items[i].ToString();  // this give you access to the row
            string stringId = null;

            stringId = stringItem.Substring(7, 1);

            int id = Int32.Parse(stringId);

            string fullString = _mainWindow.mainDataGrid.SelectedItem.ToString();
            string previousAddress = fullString.Substring(fullString.IndexOf("Direction = ") + 12, fullString.Substring(fullString.LastIndexOf("= ")).IndexOf("}") - 3);

            var group = await StepAcademyDataBase.Context.Groups.FirstOrDefaultAsync(a => a.Name == _window.textGroup.Text);
            if (group is null)
            {
                MessageBox.Show("You entered incorrect group!", "Error");
                return;
            }

            var gender = await StepAcademyDataBase.Context.Genders.FirstOrDefaultAsync(a => a.Type == _window.textGender.Text);
            if (gender is null)
            {
                MessageBox.Show("You entered incorrect gender!", "Error");
                return;
            }

            var address = await StepAcademyDataBase.Context.Addresses.FirstOrDefaultAsync(a => a.City + ", " + a.District + ", " + a.Street + ", " + a.House + ", " + a.Flat == _window.textAddress.Text);
            if (address is null)
            {
                string city = previousAddress.Substring(0, previousAddress.IndexOf(","));

                previousAddress = previousAddress.Substring(previousAddress.IndexOf(",") + 1);
                string district = previousAddress.Substring(1, previousAddress.IndexOf(",") - 1);

                previousAddress = previousAddress.Substring(previousAddress.IndexOf(",") + 1);
                string street = previousAddress.Substring(1, previousAddress.IndexOf(",") - 1);

                previousAddress = previousAddress.Substring(previousAddress.IndexOf(",") + 1);
                string house = previousAddress.Substring(1, previousAddress.IndexOf(",") - 1);

                previousAddress = previousAddress.Substring(previousAddress.IndexOf(",") + 1);
                string flat = previousAddress.Substring(previousAddress.LastIndexOf(",") + 2);


                var editAddress = await StepAcademyDataBase.Context.Addresses.FirstOrDefaultAsync(a => a.City + ", " + a.District + ", " + a.Street + ", " + a.House + ", " + a.Flat ==
                                                                                                  city + ", " + district + ", " + street + ", " + house + ", " + flat);

                string tempString = _window.textAddress.Text;

                address = new Address();

                address.City = tempString.Substring(0, tempString.IndexOf(","));

                tempString = tempString.Substring(tempString.IndexOf(",") + 1);
                address.District = tempString.Substring(1, tempString.IndexOf(",") - 1);

                tempString = tempString.Substring(tempString.IndexOf(",") + 1);
                address.Street = tempString.Substring(1, tempString.IndexOf(",") - 1);

                tempString = tempString.Substring(tempString.IndexOf(",") + 1);
                address.House = tempString.Substring(1, tempString.IndexOf(",") - 1);

                tempString = tempString.Substring(tempString.IndexOf(",") + 1);
                address.Flat = tempString.Substring(tempString.LastIndexOf(",") + 2);
                //
                // Изменение
                //
                if (editAddress != null)
                {
                    editAddress.City = address.City;
                    editAddress.District = address.District;
                    editAddress.Street = address.Street;
                    editAddress.House = address.House;
                    editAddress.Flat = address.Flat;

                    //context.Entry(editAddress).State = EntityState.Modified;
                    StepAcademyDataBase.Context.Update(editAddress);
                }
            }

            var editStudent = await StepAcademyDataBase.Context.Students.FirstOrDefaultAsync(a => a.Id == id);
            if (editStudent != null)
            {
                editStudent.FirstName = _window.textFirstName.Text;
                editStudent.LastName = _window.textLastName.Text;
                editStudent.BirthDate = Convert.ToDateTime(_window.textBirthDate.Text);
                editStudent.GradeBookNumber = _window.textGradeBookNumber.Text;
                editStudent.Note = _window.textNote.Text;
                editStudent.Phone = _window.textPhone.Text;
                editStudent.Email = _window.textEmail.Text;
                editStudent.AdmissionYear = Int16.Parse(_window.textYear.Text);
                editStudent.GroupId = group.Id;
                editStudent.GenderId = gender.Id;
                editStudent.AddressId = editStudent.AddressId;

                //StepAcademyDataBase.Context.Entry(editStudent).State = EntityState.Modified;
                StepAcademyDataBase.Context.Update(editStudent);
            }
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("Student has been successfully edited!");
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
