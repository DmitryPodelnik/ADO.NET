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
                    if (_window.Title == "Addition")
                        AddAddress(_window.textDistrict.Text, _window.textCity.Text, _window.textStreet.Text, _window.textHouse.Text, _window.textFlat.Text);
                    else
                        EditAddress();
                }));
            }
        }
        public AddressViewModel(AddressEdition window)
        {
            _window = window;

            if (_mainWindow.mainDataGrid.SelectedItem != null)
            {
                string fullString = _mainWindow.mainDataGrid.SelectedItem.ToString();

                _window.textDistrict.Text = fullString.Substring(fullString.IndexOf(";") + 1, fullString.Substring(fullString.IndexOf(";") + 1).IndexOf(";"));

                fullString = fullString.Substring(fullString.IndexOf(";") + 1);
                fullString = fullString.Substring(fullString.IndexOf(";") + 1);
                _window.textCity.Text = fullString.Substring(0, fullString.IndexOf(";"));

                fullString = fullString.Substring(fullString.IndexOf(";") + 1);
                _window.textStreet.Text = fullString.Substring(0, fullString.IndexOf(";"));

                fullString = fullString.Substring(fullString.IndexOf(";") + 1);
                _window.textHouse.Text = fullString.Substring(0, fullString.IndexOf(";"));

                _window.textFlat.Text = fullString.Substring(fullString.LastIndexOf(";") + 1);
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

        public async Task EditAddress()
        {
            int i = _mainWindow.mainDataGrid.SelectedIndex;
            string stringItem = _mainWindow.mainDataGrid.Items[i].ToString();  // this give you access to the row
            string stringId = null;

            stringId = stringItem.Substring(0, stringItem.IndexOf(";"));

            int id = Int32.Parse(stringId);

            var editAddress = await StepAcademyDataBase.Context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
            if (editAddress != null)
            {
                editAddress.District = _window.textDistrict.Text;
                editAddress.City = _window.textCity.Text;
                editAddress.Street = _window.textStreet.Text;
                editAddress.House = _window.textHouse.Text;
                editAddress.Flat = _window.textFlat.Text;

                //StepAcademyDataBase.Context.Entry(editAddress).State = EntityState.Modified;
                StepAcademyDataBase.Context.Update(editAddress);
            }
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("Address has been successfully edited!");
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
