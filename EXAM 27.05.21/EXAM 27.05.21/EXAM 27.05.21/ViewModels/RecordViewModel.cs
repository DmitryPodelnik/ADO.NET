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
    class RecordViewModel : INotifyPropertyChanged
    {
        private StepAcademy _mainWindow = (StepAcademy)Application.Current.MainWindow;
        private RecordEdition _window;

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand =
                (_saveCommand = new RelayCommand(obj =>
                {
                    AddRecord(Byte.Parse(_window.textCoins.Text), Byte.Parse(_window.textCoins.Text), _window.textSubject.Text);
                }));
            }
        }
        public RecordViewModel(RecordEdition window)
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

        public async Task AddRecord(byte coins, byte course, string subject)
        {
            var newRecord = new Record
            {
                Coins = coins,
                Course = course,
                Subject = subject
            };
            await StepAcademyDataBase.Context.Records.AddAsync(newRecord);
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("A new record has been successfully added!");

            ClearTextBoxes();

            void ClearTextBoxes()
            {
                _window.textCoins.Text = "";
                _window.textCourse.Text = "";
                _window.textSubject.Text = "";
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
