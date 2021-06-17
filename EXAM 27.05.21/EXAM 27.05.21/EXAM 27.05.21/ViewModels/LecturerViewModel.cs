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
                    AddLecturer(_window.textFirstName.Text, _window.textLastName.Text, Convert.ToDateTime(_window.textBirthDate.Text), Int32.Parse(_window.textGroup.Text));
                }));
            }
        }
        public LecturerViewModel(LecturerEdition window)
        {
            _window = window;

            if (_mainWindow.mainDataGrid.SelectedItem != null)
            {
                string fullString = _mainWindow.mainDataGrid.SelectedItem.ToString();


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
