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
    class GenderViewModel : INotifyPropertyChanged
    {
        private StepAcademy _mainWindow = (StepAcademy)Application.Current.MainWindow;
        private GenderEdition _window;

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand =
                (_saveCommand = new RelayCommand(obj =>
                {
                    if (_window.Title == "Addition")
                        AddGender(_window.textGender.Text);
                    else
                        EditGender();
                }));
            }
        }
        public GenderViewModel(GenderEdition window)
        {
            _window = window;

            if (_mainWindow.mainDataGrid.SelectedItem != null)
            {
                string fullString = _mainWindow.mainDataGrid.SelectedItem.ToString();

                _window.textGender.Text = fullString.Substring(fullString.LastIndexOf(";") + 1);
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

        public async Task EditGender()
        {
            int i = _mainWindow.mainDataGrid.SelectedIndex;
            string stringItem = _mainWindow.mainDataGrid.Items[i].ToString();  // this give you access to the row
            string stringId = null;

            stringId = stringItem.Substring(0, stringItem.IndexOf(";"));

            int id = Int32.Parse(stringId);

            var editGender = await StepAcademyDataBase.Context.Genders.FirstOrDefaultAsync(a => a.Id == id);
            if (editGender != null)
            {
                editGender.Type = _window.textGender.Text;

                //StepAcademyDataBase.Context.Entry(editGender).State = EntityState.Modified;
                StepAcademyDataBase.Context.Update(editGender);
            }
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("Gender has been successfully edited!");
        }

        public async Task AddGender(string gender)
        {
            var newGender = new Gender
            {
                Type = gender,
                Students = null
            };
            await StepAcademyDataBase.Context.Genders.AddAsync(newGender);
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("A new gender has been successfully added!");

            ClearTextBoxes();

            void ClearTextBoxes()
            {
                _window.textGender.Text = "";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
