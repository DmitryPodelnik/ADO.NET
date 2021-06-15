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
using EXAM_27._05._21.ViewModels;

namespace EXAM_27._05._21
{
    class AcademyViewModel : INotifyPropertyChanged
    {
        //private StepAcademyDataBase _database = new();
        private StepAcademy _mainWindow = (StepAcademy)Application.Current.MainWindow;
        private AcademyEdition _window;

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand =
                (_saveCommand = new RelayCommand(obj =>
                {
                    AddItem();
                }));
            }
        }
        public AcademyViewModel(AcademyEdition window)
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
        public void AddItem()
        {
            switch (((StepAcademy)Application.Current.MainWindow).chooseTable.SelectedItem.ToString())
            {
                case "Academies":

                    AddAcademy(_window.textCity.Text, _window.textStreet.Text, _window.textHouse.Text);

                    break;            
            }
        }

        public async Task AddAcademy(string city, string street, string house)
        {
            var newAcademy = new Academy
            {
                City = city,
                Street = street,
                House = house,
                AcademyPhones = null
            };
            await StepAcademyDataBase.Context.Academies.AddAsync(newAcademy);
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("A new academy has been successfully added!");

            ClearTextBoxes();

            void ClearTextBoxes()
            {
                _window.textCity.Text = "";
                _window.textStreet.Text = "";
                _window.textHouse.Text = "";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
