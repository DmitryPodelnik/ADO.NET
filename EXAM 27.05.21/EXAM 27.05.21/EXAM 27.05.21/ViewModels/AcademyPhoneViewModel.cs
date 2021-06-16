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
    class AcademyPhoneViewModel : INotifyPropertyChanged
    {
        private StepAcademy _mainWindow = (StepAcademy)Application.Current.MainWindow;
        private AcademyPhoneEdition _window;

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand =
                (_saveCommand = new RelayCommand(obj =>
                {
                    AddAcademyPhone(_window.textPhone.Text, Int32.Parse(_window.textAcademy.Text));
                }));
            }
        }
        public AcademyPhoneViewModel(AcademyPhoneEdition window)
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

        public async Task AddAcademyPhone(string phone, int academy)
        {
            var newAcademyPhone = new AcademyPhone
            {
                Phone = phone,
                AcademyId = academy
            };
            await StepAcademyDataBase.Context.AcademyPhones.AddAsync(newAcademyPhone);
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("A new academy phone has been successfully added!");

            ClearTextBoxes();

            void ClearTextBoxes()
            {
                _window.textPhone.Text = "";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
