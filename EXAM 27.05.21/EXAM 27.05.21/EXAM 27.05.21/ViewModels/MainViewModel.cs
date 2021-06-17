using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EXAM_27._05._21.Models;
using EXAM_27._05._21.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EXAM_27._05._21.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private StepAcademyDataBase _database = new();
        public List<string> Tables { get; set; } = new();

        private string _selectedTable;
        public string SelectedTable
        {
            get { return _selectedTable; }
            set
            {
                _selectedTable = value;
                RefreshDataGrid(_selectedTable);
                _mainWindow.addButton.IsEnabled = true;
            }
        }

        private StepAcademy _mainWindow = (StepAcademy)Application.Current.MainWindow;

        public MainViewModel()
        {
            Tables = _database.GetTables(Tables);
            Tables.Remove("Grades");
        }

        private async Task RefreshDataGrid(string table)
        {
            await ShowObjectsAsync(table);
        }

        private async Task ShowObjectsAsync(string table)
        {
            switch (table)
            {
                case "Academies":

                    _mainWindow.mainDataGrid.ItemsSource = await StepAcademyDataBase.Context.Academies.ToListAsync();

                    break;

                case "Academies' Phones":

                    StepAcademyDataBase.GetAllAcademyPhones(ref _mainWindow);

                    break;

                case "Addresses":

                    _mainWindow.mainDataGrid.ItemsSource = await StepAcademyDataBase.Context.Addresses.ToListAsync();

                    break;

                case "Records":

                    _mainWindow.mainDataGrid.ItemsSource = await StepAcademyDataBase.Context.Records.ToListAsync();

                    break;

                case "Genders":

                    _mainWindow.mainDataGrid.ItemsSource = await StepAcademyDataBase.Context.Genders.ToListAsync();

                    break;

                case "Groups":

                    StepAcademyDataBase.GetAllGroups(ref _mainWindow);

                    break;

                case "Leaders":

                    StepAcademyDataBase.GetAllLeaders(ref _mainWindow);

                    break;

                case "Lecturers":

                    StepAcademyDataBase.GetAllLecturers(ref _mainWindow);

                    break;

                case "Specialties":

                    _mainWindow.mainDataGrid.ItemsSource = await StepAcademyDataBase.Context.Specialties.ToListAsync();

                    break;

                case "Students":

                    StepAcademyDataBase.GetAllStudents(ref _mainWindow);

                    break;

                case "Students'Grades":

                    StepAcademyDataBase.GetAllStudentsGrades(ref _mainWindow);

                    break;

                case "Subjects":

                    _mainWindow.mainDataGrid.ItemsSource = await StepAcademyDataBase.Context.Subjects.ToListAsync();

                    break;
            }
            _mainWindow.mainDataGrid.Items.Refresh();
        }

        private RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand =
                (_addCommand = new RelayCommand(obj =>
                {
                    AddItem();
                }));
            }
        }

        private RelayCommand _deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return _deleteCommand ??
                    (_deleteCommand = new RelayCommand(obj =>
                    {
                        _database.DeleteItem(SelectedTable, _mainWindow);
                    }));
            }
        }

        private RelayCommand _editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return _editCommand =
                (_editCommand = new RelayCommand(obj =>
                {
                    AddItem("Edition", "Save");
                }));
            }
        }

        private void AddItem(string title = "Addition", string buttonName = "Add")
        {
            switch (SelectedTable)
            {
                case "Academies":
                    {
                        AcademyEdition _window = new();
                        _window.Title = title;
                        _window.okButton.Content = buttonName;
                        _window.ShowDialog();
                    }

                    break;

                case "Academies' Phones":
                    {
                        AcademyPhoneEdition _window = new();
                        _window.Title = title;
                        _window.okButton.Content = buttonName;
                        _window.ShowDialog();
                    }

                    break;

                case "Addresses":
                    {
                        AddressEdition _window = new();
                        _window.Title = title;
                        _window.okButton.Content = buttonName;
                        _window.ShowDialog();
                    }


                    break;

                case "Records":
                    {
                        RecordEdition _window = new();
                        _window.Title = title;
                        _window.okButton.Content = buttonName;
                        _window.ShowDialog();
                    }


                    break;

                case "Genders":
                    {
                        GenderEdition _window = new();
                        _window.Title = title;
                        _window.okButton.Content = buttonName;
                        _window.ShowDialog();
                    }


                    break;

                case "Groups":
                    {
                        GroupEdition _window = new();
                        _window.Title = title;
                        _window.okButton.Content = buttonName;
                        _window.ShowDialog();
                    }


                    break;

                case "Leaders":
                    {
                        LeaderEdition _window = new();
                        _window.Title = title;
                        _window.okButton.Content = buttonName;
                        _window.ShowDialog();
                    }


                    break;

                case "Lecturers":
                    {
                        LecturerEdition _window = new();
                        _window.Title = title;
                        _window.okButton.Content = buttonName;
                        _window.ShowDialog();
                    }


                    break;

                case "Specialties":
                    {
                        SpecialtyEdition _window = new();
                        _window.Title = title;
                        _window.okButton.Content = buttonName;
                        _window.ShowDialog();
                    }


                    break;

                case "Students":
                    {
                        StudentEdition _window = new();
                        _window.Title = title;
                        _window.okButton.Content = buttonName;
                        _window.ShowDialog();
                    }


                    break;

                case "Students'Grades":
                    {
                        StudentGradeEdition _window = new();
                        _window.Title = title;
                        _window.okButton.Content = buttonName;
                        _window.ShowDialog();
                    }


                    break;

                case "Subjects":
                    {
                        SubjectEdition _window = new();
                        _window.Title = title;
                        _window.okButton.Content = buttonName;
                        _window.ShowDialog();
                    }


                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
