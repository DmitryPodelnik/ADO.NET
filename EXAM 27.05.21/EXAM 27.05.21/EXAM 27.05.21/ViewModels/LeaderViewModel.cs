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
    class LeaderViewModel : INotifyPropertyChanged
    {
        private StepAcademy _mainWindow = (StepAcademy)Application.Current.MainWindow;
        private LeaderEdition _window;

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand =
                (_saveCommand = new RelayCommand(obj =>
                {
                    AddLeader(Int32.Parse(_window.textStudent.Text), Int32.Parse(_window.textGroup.Text));
                }));
            }
        }
        public LeaderViewModel(LeaderEdition window)
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

        public async Task AddLeader(int studentID, int groupID)
        {
            var newLeader = new Leader
            {
                StudentId = studentID,
                GroupId = groupID
            };
            await StepAcademyDataBase.Context.Leaders.AddAsync(newLeader);
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("A new leader has been successfully added!");

            ClearTextBoxes();

            void ClearTextBoxes()
            {
                _window.textStudent.Text = "";
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
