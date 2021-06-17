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
    class SpecialtyViewModel : INotifyPropertyChanged
    {
        private StepAcademy _mainWindow = (StepAcademy)Application.Current.MainWindow;
        private SpecialtyEdition _window;

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand =
                (_saveCommand = new RelayCommand(obj =>
                {
                    if (_window.Title == "Addition")
                        AddSpecialty(Int16.Parse(_window.textCode.Text), _window.textName.Text);
                    else
                        EditSpecialty();
                }));
            }
        }
        public SpecialtyViewModel(SpecialtyEdition window)
        {
            _window = window;

            if (_mainWindow.mainDataGrid.SelectedItem != null)
            {
                string fullString = _mainWindow.mainDataGrid.SelectedItem.ToString();

                _window.textCode.Text = fullString.Substring(fullString.IndexOf(";") + 1, fullString.Substring(fullString.IndexOf(";") + 1).IndexOf(";"));
                _window.textName.Text = fullString.Substring(fullString.LastIndexOf(";") + 1);
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

        public async Task EditSpecialty()
        {
            int i = _mainWindow.mainDataGrid.SelectedIndex;
            string stringItem = _mainWindow.mainDataGrid.Items[i].ToString();  // this give you access to the row
            string stringId = null;

            stringId = stringItem.Substring(0, stringItem.IndexOf(";"));

            int id = Int32.Parse(stringId);

            var editSpecialty = await StepAcademyDataBase.Context.Specialties.FirstOrDefaultAsync(a => a.Id == id);
            if (editSpecialty != null)
            {
                editSpecialty.SpecialtyCode = Int16.Parse(_window.textCode.Text);
                editSpecialty.Name = _window.textName.Text;

                //StepAcademyDataBase.Context.Entry(editSpecialty).State = EntityState.Modified;
                StepAcademyDataBase.Context.Update(editSpecialty);
            }
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("Specialty has been successfully edited!");
        }

        public async Task AddSpecialty(short specialtyCode, string name)
        {
            var newSpecialty = new Specialty
            {
                SpecialtyCode = specialtyCode,
                Name = name
            };
            await StepAcademyDataBase.Context.Specialties.AddAsync(newSpecialty);
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("A new specialty has been successfully added!");

            ClearTextBoxes();

            void ClearTextBoxes()
            {
                _window.textName.Text = "";
                _window.textCode.Text = "";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
