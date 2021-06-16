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
    class AddressViewModel : INotifyPropertyChanged
    {
        private StepAcademy _mainWindow = (StepAcademy)Application.Current.MainWindow;
        private AddressEdition _window;

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand =
                (_saveCommand = new RelayCommand(obj =>
                {
                    AddAddress(_window.textDistrict.Text, _window.textCity.Text, _window.textStreet.Text, _window.textHouse.Text, _window.textFlat.Text);
                }));
            }
        }
        public AddressViewModel(AddressEdition window)
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

        public async Task AddAddress(string district, string city, string street, string house, string flat)
        {
            var newAddress = new Address
            {
                District = district,
                City = city,
                Street = street,
                House = house,
                Flat = flat,
                Students = null
            };
            await StepAcademyDataBase.Context.Addresses.AddAsync(newAddress);
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("A new address has been successfully added!");

            ClearTextBoxes();

            void ClearTextBoxes()
            {
                _window.textDistrict.Text = "";
                _window.textCity.Text = "";
                _window.textStreet.Text = "";
                _window.textHouse.Text = "";
                _window.textFlat.Text = "";
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
