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
    class SubjectViewModel : INotifyPropertyChanged
    {
        private StepAcademy _mainWindow = (StepAcademy)Application.Current.MainWindow;
        private SubjectEdition _window;

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand =
                (_saveCommand = new RelayCommand(obj =>
                {
                    AddSubject(_window.textName.Text, Byte.Parse(_window.textCourse.Text), Int16.Parse(_window.textHours.Text));
                }));
            }
        }
        public SubjectViewModel(SubjectEdition window)
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

        public async Task AddSubject(string name, byte course, short hours)
        {
            var newSubject = new Subject
            {
                Name = name,
                Course = course,
                Hours = hours
            };
            await StepAcademyDataBase.Context.Subjects.AddAsync(newSubject);
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("A new subject has been successfully added!");

            ClearTextBoxes();

            void ClearTextBoxes()
            {
                _window.textName.Text = "";
                _window.textCourse.Text = "";
                _window.textHours.Text = "";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
