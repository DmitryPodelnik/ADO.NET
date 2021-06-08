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
        //private StepAcademyContext _context;
        public List<string> Tables { get; set; } = new();
        public List<Academy> Academies { get; set; } = new();

        private string _selectedTable;
        public string SelectedTable
        {
            get { return _selectedTable; }
            set
            {
                _selectedTable = value;
                RefreshDataGrid();
            }
        }

        private AcademyViewModel _academyViewModel;
        private AddressViewModel _addressViewModel;
        private CreditViewModel _creditViewModel;
        private GenderViewModel _genderViewModel;
        private GradeViewModel _gradeViewModel;
        private GroupDescription _groupViewModel;
        private LeaderViewModel _leaderViewModel;
        private LecturerViewModel _lecturerViewModel;
        private SpecialtyViewModel _specialtyViewModel;
        private StudentGradeViewModel _studentGradeViewModel;
        private StudentViewModel _studentViewModel;
        private SubjectViewModel _subjectViewModel;

        private StepAcademy _mainWindow = (StepAcademy)Application.Current.MainWindow;

        public MainViewModel()
        {
            Tables = _database.GetTables(Tables);
            //_context = new StepAcademyContext(_database.Options);
        }

        private async Task RefreshDataGrid()
        {
            await ShowObjectsAsync();
        }

        private async Task ShowObjectsAsync()
        {
            using (var _context = new StepAcademyContext(_database.Options))
            {
                switch (_mainWindow.chooseTable.SelectedItem.ToString())
                {
                    case "Academies":

                        _mainWindow.mainDataGrid.ItemsSource = await _context.Academies.ToListAsync();

                        break;

                    case "Academies' Phones":

                        _mainWindow.mainDataGrid.ItemsSource = await _context.Academies.ToListAsync();

                        break;

                    case "Addresses":

                        _mainWindow.mainDataGrid.ItemsSource = await _context.Addresses.ToListAsync();

                        break;

                    case "Credits":

                        _mainWindow.mainDataGrid.ItemsSource = await _context.Credits.ToListAsync();

                        break;

                    case "Genders":

                        _mainWindow.mainDataGrid.ItemsSource = await _context.Genders.ToListAsync();

                        break;

                    case "Grades":

                        _mainWindow.mainDataGrid.ItemsSource = await _context.Grades.ToListAsync();

                        break;

                    case "Groups":

                        _mainWindow.mainDataGrid.ItemsSource = await _context.Groups.ToListAsync();

                        break;

                    case "Leaders":

                        _mainWindow.mainDataGrid.ItemsSource = await _context.Leaders.ToListAsync();

                        break;

                    case "Lecturers":

                        _mainWindow.mainDataGrid.ItemsSource = await _context.Lecturers.ToListAsync();

                        break;

                    case "Specialties":

                        _mainWindow.mainDataGrid.ItemsSource = await _context.Specialties.ToListAsync();

                        break;

                    case "Students":

                        _mainWindow.mainDataGrid.ItemsSource = await _context.Students.ToListAsync();

                        break;

                    case "Students'Grades":

                        _mainWindow.mainDataGrid.ItemsSource = await _context.StudentGrades.ToListAsync();

                        break;

                    case "Subjects":

                        _mainWindow.mainDataGrid.ItemsSource = await _context.Subjects.ToListAsync();

                        break;
                }
            }
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
