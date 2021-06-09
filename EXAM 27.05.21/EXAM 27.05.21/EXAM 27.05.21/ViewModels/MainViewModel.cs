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
using EXAM_27._05._21.Models;
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
            }
        }

        // For future versions
        //
        //private AcademyViewModel _academyViewModel;
        //private AddressViewModel _addressViewModel;
        //private CreditViewModel _creditViewModel;
        //private GenderViewModel _genderViewModel;
        //private GradeViewModel _gradeViewModel;
        //private GroupDescription _groupViewModel;
        //private LeaderViewModel _leaderViewModel;
        //private LecturerViewModel _lecturerViewModel;
        //private SpecialtyViewModel _specialtyViewModel;
        //private StudentGradeViewModel _studentGradeViewModel;
        //private StudentViewModel _studentViewModel;
        //private SubjectViewModel _subjectViewModel;

        private StepAcademy _mainWindow = (StepAcademy)Application.Current.MainWindow;

        public MainViewModel()
        {
            Tables = _database.GetTables(Tables);
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

                    _mainWindow.mainDataGrid.ItemsSource = await _database.Context.Academies.ToListAsync();

                    break;

                case "Academies' Phones":

                    _mainWindow.mainDataGrid.ItemsSource = await _database.Context.Academies.ToListAsync();

                    break;

                case "Addresses":

                   _mainWindow.mainDataGrid.ItemsSource = await _database.Context.Addresses.ToListAsync();

                    break;

                case "Credits":

                    _mainWindow.mainDataGrid.ItemsSource = await _database.Context.Credits.ToListAsync();

                    break;

                case "Genders":

                    _mainWindow.mainDataGrid.ItemsSource = await _database.Context.Genders.ToListAsync();

                    break;

                case "Grades":

                    _mainWindow.mainDataGrid.ItemsSource = await _database.Context.Grades.ToListAsync();

                    break;

                case "Groups":

                    _mainWindow.mainDataGrid.ItemsSource = await _database.Context.Groups.ToListAsync();

                    break;

                case "Leaders":

                    _mainWindow.mainDataGrid.ItemsSource = await _database.Context.Leaders.ToListAsync();

                    break;

                case "Lecturers":

                    _mainWindow.mainDataGrid.ItemsSource = await _database.Context.Lecturers.ToListAsync();

                    break;

                case "Specialties":

                    _mainWindow.mainDataGrid.ItemsSource = await _database.Context.Specialties.ToListAsync();

                    break;

                case "Students":

                    ObservableCollection<Student> students = new();
                    //_mainWindow.mainDataGrid.ItemsSource = await _database.Context.Students.ToListAsync();

                    await _database.Context.Students.ForEachAsync(student =>
                    {
                        var newStudent = new Student
                        {
                            Id = student.Id,
                            FirstName = student.FirstName,
                            LastName = student.LastName,
                            BirthDate = student.BirthDate,
                            GradeBookNumber = student.GradeBookNumber,
                            Note = student.Note,
                            Phone = student.Phone,
                            Email = student.Email,
                            AdmissionYear = student.AdmissionYear,
                            GroupId = student.GroupId,
                            GenderId = student.GenderId,
                            SpecialtyId = student.SpecialtyId,
                            AddressId = student.AddressId //== _database.Context.Addresses.Find(student.AddressId) ? student.AddressId : student.AddressId
                        };

                        students.Add(newStudent);
                    });
                    _mainWindow.mainDataGrid.ItemsSource = students;

                    break;

                case "Students'Grades":

                    _mainWindow.mainDataGrid.ItemsSource = await _database.Context.StudentGrades.ToListAsync();

                    break;

                case "Subjects":

                    _mainWindow.mainDataGrid.ItemsSource = await _database.Context.Subjects.ToListAsync();

                    break;
            }
            _mainWindow.mainDataGrid.Items.Refresh();
        }

        private RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand;
                //return _addCommand ??
                //  (_addCommand = new RelayCommand(obj =>
                //  {
                //      Phone phone = new Phone();
                //      Phones.Insert(0, phone);
                //      SelectedPhone = phone;
                //  }));
            }
        }

        private RelayCommand _deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return _deleteCommand;
                //return _deleteCommand ??
                //  (_deleteCommand = new RelayCommand(obj =>
                //  {
                //      Phone phone = new Phone();
                //      Phones.Insert(0, phone);
                //      SelectedPhone = phone;
                //  }));
            }
        }

        private RelayCommand _editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return _editCommand;
                //return _editCommand ??
                //  (_editCommand = new RelayCommand(obj =>
                //  {
                //      Phone phone = new Phone();
                //      Phones.Insert(0, phone);
                //      SelectedPhone = phone;
                //  }));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
