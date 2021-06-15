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
        private StepAcademyDataBase _database = new();
        private StepAcademy _mainWindow = (StepAcademy)Application.Current.MainWindow;
        string _city;
        string _street;
        string _house;

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand =
                (_saveCommand = new RelayCommand(obj =>
                {
                    AddAcademy(_city, _street, _house);
                }));
            }
        }

        public AcademyViewModel()
        {
            
        }
        public AcademyViewModel(string city, string street, string house)
        {
            _city = city;
            _street = street;
            _house = house;
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
            await _database.Context.Academies.AddAsync(newAcademy);
            await _database.Context.SaveChangesAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
