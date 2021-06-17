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
using Microsoft.EntityFrameworkCore;

namespace EXAM_27._05._21
{
    class AcademyViewModel : INotifyPropertyChanged
    {
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
                    if (_window.Title == "Addition")
                        AddAcademy(_window.textCity.Text, _window.textStreet.Text, _window.textHouse.Text);
                    else
                        EditAcademy();
                }));
            }
        }
        public AcademyViewModel(AcademyEdition window)
        {
            _window = window;

            if (_mainWindow.mainDataGrid.SelectedItem != null)
            {
                string fullString = _mainWindow.mainDataGrid.SelectedItem.ToString();

                _window.textCity.Text = fullString.Substring(fullString.IndexOf(";") + 1, fullString.Substring(fullString.IndexOf(";") + 1).IndexOf(";"));
                _window.textStreet.Text = fullString.Substring(fullString.Substring(fullString.IndexOf(";") + 1).IndexOf(";") + 3, fullString.Substring(fullString.Substring(fullString.IndexOf(";") + 1).IndexOf(";") + 3).IndexOf(";"));
                _window.textHouse.Text = fullString.Substring(fullString.LastIndexOf(";") + 1);
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

        public async Task EditAcademy()
        {
            int i = _mainWindow.mainDataGrid.SelectedIndex;
            string stringItem = _mainWindow.mainDataGrid.Items[i].ToString();  // this give you access to the row
            string stringId = null;

            stringId = stringItem.Substring(0, stringItem.IndexOf(";"));

            int id = Int32.Parse(stringId);

            var editAcademy = await StepAcademyDataBase.Context.Academies.FirstOrDefaultAsync(a => a.Id == id);
            if (editAcademy != null)
            {
                editAcademy.City = _window.textCity.Text;
                editAcademy.Street = _window.textStreet.Text;
                editAcademy.House = _window.textHouse.Text;

                //StepAcademyDataBase.Context.Entry(editAcademy).State = EntityState.Modified;
                StepAcademyDataBase.Context.Update(editAcademy);
            }
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("Academy has been successfully edited!");
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
