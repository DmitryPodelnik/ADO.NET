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
    class GroupViewModel : INotifyPropertyChanged
    {
        private StepAcademy _mainWindow = (StepAcademy)Application.Current.MainWindow;
        private GroupEdition _window;

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand =
                (_saveCommand = new RelayCommand(obj =>
                {
                    if (_window.Title == "Addition")
                        AddGroup(_window.textName.Text, Byte.Parse(_window.textClass.Text), Int32.Parse(_window.textSpecialty.Text));
                    else
                        EditGroup();
                }));
            }
        }
        public GroupViewModel(GroupEdition window)
        {
            _window = window;

            if (_mainWindow.mainDataGrid.SelectedItem != null)
            {
                string fullString = _mainWindow.mainDataGrid.SelectedItem.ToString();

                _window.textName.Text = fullString.Substring(fullString.IndexOf("Name = ") + 7, fullString.IndexOf(","));

                _window.textClass.Text = fullString.Substring(fullString.IndexOf("Class = ") + 8, 1);

                _window.textSpecialty.Text = fullString.Substring(fullString.LastIndexOf("= ") + 2, fullString.Substring(fullString.LastIndexOf("= ")).IndexOf("}") - 3);
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

        public async Task EditGroup()
        {
            int i = _mainWindow.mainDataGrid.SelectedIndex;
            string stringItem = _mainWindow.mainDataGrid.Items[i].ToString();  // this give you access to the row
            string stringId = null;

            stringId = stringItem.Substring(7, 1);

            int id = Int32.Parse(stringId);

            var specialty = await StepAcademyDataBase.Context.Specialties.FirstOrDefaultAsync(a => a.Name == _window.textSpecialty.Text);  
            if (specialty is null)
            {
                MessageBox.Show("You entered incorrect specialty", "Error");
                return;
            }

            var editGroup = await StepAcademyDataBase.Context.Groups.FirstOrDefaultAsync(a => a.Id == id);
            if (editGroup != null)
            {
                editGroup.Name = _window.textName.Text;
                editGroup.Class = Byte.Parse(_window.textClass.Text);
                editGroup.SpecialtyId = specialty.Id;

                //StepAcademyDataBase.Context.Entry(editGroup).State = EntityState.Modified;
                StepAcademyDataBase.Context.Update(editGroup);
            }
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("Group has been successfully edited!");
        }

        public async Task AddGroup(string name, byte group, int specialtyId)
        {
            var newGroup = new Group
            {
               Name = name,
               Class = group,
               SpecialtyId = specialtyId
            };
            await StepAcademyDataBase.Context.Groups.AddAsync(newGroup);
            await StepAcademyDataBase.Context.SaveChangesAsync();

            MessageBox.Show("A new group has been successfully added!");

            ClearTextBoxes();

            void ClearTextBoxes()
            {
                _window.textName.Text = "";
                _window.textClass.Text = "";
                _window.textSpecialty.Text = "";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
