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
                    AddGroup(_window.textName.Text, Byte.Parse(_window.textClass.Text), Int32.Parse(_window.textSpecialty.Text));
                }));
            }
        }
        public GroupViewModel(GroupEdition window)
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
