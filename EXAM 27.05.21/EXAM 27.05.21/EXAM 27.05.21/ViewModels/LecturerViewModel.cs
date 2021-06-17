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
    class LecturerViewModel : INotifyPropertyChanged
    {
        private StepAcademy _mainWindow = (StepAcademy)Application.Current.MainWindow;
        private LecturerEdition _window;

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand =
                (_saveCommand = new RelayCommand(obj =>
                {
                    if (_window.Title == "Addition")
                        AddLecturer(_window.textFirstName.Text, _window.textLastName.Text, Convert.ToDateTime(_window.textBirthDate.Text), Int32.Parse(_window.textGroup.Text));
                    else
                        EditLecturer();
                }));
            }
        }
        public LecturerViewModel(LecturerEdition window)
        {
            _window = window;

            if (_mainWindow.mainDataGrid.SelectedItem != null)
            {
                string fullString = _mainWindow.mainDataGrid.SelectedItem.ToString();

                _window.textFirstName.Text = fullString.Substring(fullString.IndexOf("FirstName = ") + 12, fullString.Substring(fullString.IndexOf("FirstName = ") + 12).IndexOf(","));
                _window.textLastName.Text = fullString.Substring(fullString.IndexOf("LastName = ") + 11, fullString.Substring(fullString.IndexOf("LastName = ") + 11).IndexOf(","));
                _window.textBirthDate.Text = fullString.Substring(fullString.IndexOf("BirthDate = ") + 12, fullString.Substring(fullString.IndexOf("BirthDate = ") + 12).IndexOf(","));
                _window.textGroup.Text = fullString.Substring(fullString.IndexOf("Class = ") + 8, (fullString.Substring(fullString.LastIndexOf("= ")).IndexOf("}") - 3));
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

        public async Task EditLecturer()
        {
            int i = _mainWindow.mainDataGrid.SelectedIndex;
            string stringItem = _mainWindow.mainDataGrid.Items[i].ToString();  // this give you access to the row
            string stringId = null;

            stringId = stringItem.Substring(7, 1);

            int id = Int32.Parse(stringId);

            var group = await StepAcademyDataBase.Context.Groups.FirstOrDefaultAsync(a => a.Name == _window.textGroup.Text);
            if (group is null)
            {
                MessageBox.Show("You entered incorrect group!", "Error");
                return;
            }

            var editLecturer = await StepAcademyDataBase.Context.Lecturers.FirstOrDefaultAsync(a => a.Id == id);
            if (editLecturer != null)
            {
                editLecturer.FirstName = _window.textFirstName.Text;
                editLecturer.LastName = _window.textLastName.Text;
                editLecturer.BirthDate = Convert.ToDateTime(_window.textBirthDate.Text);
                editLecturer.GroupId = group.Id;

                //StepAcademyDataBase.Context.Entry(editLecturer).State = EntityState.Modified;
                StepAcademyDataBase.Context.Update(editLecturer);
            }
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("Lecturer has been successfully edited!");
        }

        public async Task AddLecturer(string firstName, string lastName, DateTime birthDate, int groupId)
        {
            var newLecturer = new Lecturer
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate,
                GroupId = groupId
            };
            await StepAcademyDataBase.Context.Lecturers.AddAsync(newLecturer);
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("A new lecturer has been successfully added!");

            ClearTextBoxes();

            void ClearTextBoxes()
            {
                _window.textFirstName.Text = "";
                _window.textLastName.Text = "";
                _window.textBirthDate.Text = "";
                _window.textGroup.Text = "";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
