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
                    if (_window.Title == "Addition")
                        AddAcademyPhone(_window.textPhone.Text, Int32.Parse(_window.textAcademy.Text));
                    else
                        EditAcademyPhone();
                }));
            }
        }
        public AcademyPhoneViewModel(AcademyPhoneEdition window)
        {
            _window = window;

            if (_mainWindow.mainDataGrid.SelectedItem != null)
            {
                string fullString = _mainWindow.mainDataGrid.SelectedItem.ToString();

                _window.textPhone.Text = fullString.Substring(fullString.IndexOf("Phone = ") + 8, fullString.Substring(fullString.IndexOf("Phone = ") + 8).IndexOf("}") - 1);

                string city = fullString.Substring(fullString.IndexOf("AcademyCity = ") + 14, fullString.Substring(fullString.IndexOf("AcademyCity = ") + 14).IndexOf(","));
                string street = fullString.Substring(fullString.IndexOf("AcademyStreet = ") + 16, fullString.Substring(fullString.IndexOf("AcademyStreet = ") + 16).IndexOf(","));
                string house = fullString.Substring(fullString.IndexOf("AcademyHouse = ") + 15, fullString.Substring(fullString.IndexOf("AcademyHouse = ") + 15).IndexOf(","));

                _window.textAcademy.Text = city + ", " + street + ", " + house;
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

        public async Task EditAcademyPhone()
        {
            int i = _mainWindow.mainDataGrid.SelectedIndex;
            string stringItem = _mainWindow.mainDataGrid.Items[i].ToString();  // this give you access to the row
            string stringId = null;

            stringId = stringItem.Substring(7, 1);

            int id = Int32.Parse(stringId);

            var editAcademyPhone = await StepAcademyDataBase.Context.AcademyPhones.FirstOrDefaultAsync(a => a.Id == id);
            if (editAcademyPhone != null)
            {
                editAcademyPhone.Phone = _window.textPhone.Text;

                //StepAcademyDataBase.Context.Entry(editAcademyPhone).State = EntityState.Modified;
                StepAcademyDataBase.Context.Update(editAcademyPhone);
            }
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("Academy phone has been successfully edited!");
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
                _window.textAcademy.Text = "";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
