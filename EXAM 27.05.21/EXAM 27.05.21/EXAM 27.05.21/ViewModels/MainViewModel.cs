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

                    _mainWindow.mainDataGrid.ItemsSource = await _database.Context.Academies.ToListAsync();
                    break;

                case "Academies' Phones":

                    _mainWindow.mainDataGrid.ItemsSource = _database.Context.AcademyPhones
                        .Join(
                                 _database.Context.Academies,
                                 u => u.AcademyId,
                                 c => c.Id,
                                 (u, c) => new
                                 {
                                     Id = u.Id,
                                     AcademyCity = c.City,
                                     AcademyStreet = c.Street,
                                     AcademyHouse = c.House,
                                     Phone = u.Phone
                                 }).ToList();


                    break;

                case "Addresses":

                    _mainWindow.mainDataGrid.ItemsSource = await _database.Context.Addresses.ToListAsync();

                    break;

                case "Records":

                    _mainWindow.mainDataGrid.ItemsSource = await _database.Context.Records.ToListAsync();

                    break;

                case "Genders":

                    _mainWindow.mainDataGrid.ItemsSource = await _database.Context.Genders.ToListAsync();

                    break;

                case "Groups":

                    _mainWindow.mainDataGrid.ItemsSource = _database.Context.Groups
                             .Join(
                                    _database.Context.Specialties,
                                    g => g.SpecialtyId,
                                    c => c.Id,
                                    (g, c) => new
                                    {
                                        Id = g.Id,
                                        Name = g.Name,
                                        Class = g.Class,
                                        Speciality = c.Name
                                    }).ToList();

                    break;

                case "Leaders":

                    _mainWindow.mainDataGrid.ItemsSource = _database.Context.Leaders
                            .Join(
                                    _database.Context.Students,
                                    l => l.StudentId,
                                    s => s.Id,
                                    (l, s) => new
                                    {
                                        Id = l.Id,
                                        Student = s.FirstName + " " + s.LastName,
                                        GroupId = l.GroupId
                                    }).Join(
                                            _database.Context.Groups,
                                            l => l.GroupId,
                                            g => g.Id,
                                            (l, g) => new
                                            {
                                                Id = l.Id,
                                                Student = l.Student,
                                                Group = g.Name
                                            }).ToList();

                    break;

                case "Lecturers":

                    _mainWindow.mainDataGrid.ItemsSource = _database.Context.Lecturers
                            .Join(
                                    _database.Context.Groups,
                                    l => l.GroupId,
                                    g => g.Id,
                                    (l, g) => new
                                    {
                                        Id = l.Id,
                                        FirstName = l.FirstName,
                                        LastName = l.LastName,
                                        BirthDate = l.BirthDate,
                                        Class = g.Name
                                    }).ToList();

                    break;

                case "Specialties":

                    _mainWindow.mainDataGrid.ItemsSource = await _database.Context.Specialties.ToListAsync();

                    break;

                case "Students":

                    _mainWindow.mainDataGrid.ItemsSource = _database.Context.Students
                            .Join(
                                    _database.Context.Genders,
                                    s => s.GenderId,
                                    g => g.Id,
                                    (s, g) => new
                                    {
                                        Id = s.Id,
                                        FirstName = s.FirstName,
                                        LastName = s.LastName,
                                        BirthDate = s.BirthDate,
                                        GradeBookNumber = s.GradeBookNumber,
                                        Note = s.Note,
                                        Phone = s.Phone,
                                        Email = s.Email,
                                        AdmissionYear = s.AdmissionYear,
                                        GroupId = s.GroupId,
                                        Sex = g.Type,
                                        AddressId = s.AddressId
                                    }).Join(
                                                _database.Context.Groups,
                                                g => g.GroupId,
                                                gr => gr.Id,
                                                (g, gr) => new
                                                {
                                                    Id = g.Id,
                                                    FirstName = g.FirstName,
                                                    LastName = g.LastName,
                                                    BirthDate = g.BirthDate,
                                                    GradeBookNumber = g.GradeBookNumber,
                                                    Note = g.Note,
                                                    Phone = g.Phone,
                                                    Email = g.Email,
                                                    AdmissionYear = g.AdmissionYear,
                                                    Class = gr.Name,
                                                    Sex = g.Sex,
                                                    AddressId = g.AddressId
                                                }).Join(
                                                            _database.Context.Addresses,
                                                            g => g.AddressId,
                                                            addr => addr.Id,
                                                            (g, addr) => new
                                                            {
                                                                Id = g.Id,
                                                                FirstName = g.FirstName,
                                                                LastName = g.LastName,
                                                                BirthDate = g.BirthDate,
                                                                GradeBookNumber = g.GradeBookNumber,
                                                                Note = g.Note,
                                                                Phone = g.Phone,
                                                                Email = g.Email,
                                                                AdmissionYear = g.AdmissionYear,
                                                                Class = g.Class,
                                                                Sex = g.Sex,
                                                                Direction = addr.City + ", " + addr.District + ", " + addr.Street + ", " + addr.House + ", " + addr.Flat
                                                            }).ToList();

                    break;

                case "Students'Grades":

                    //_mainWindow.mainDataGrid.ItemsSource = _database.Context.Grades
                    //         .Join(
                    //             (_database.Context.StudentGrades.Join(
                    //                _database.Context.Students,
                    //                sg => sg.StudentId,
                    //                student => student.Id,
                    //                (sg, student) => new
                    //                {
                    //                    Id = sg.Id,
                    //                    Student = student.FirstName + " " + student.LastName,
                    //                    Grades = sg.Grades.Count

                    //                })),
                    //             g => g.Id,
                    //             sgs => sgs.Grades,
                    //             (g, sgs) => new
                    //             {
                    //                 Id = sgs.Id,
                    //                 Student = sgs.Student,
                    //                 Grades = g.Mark + "/" + g.Value
                    //             });


                    var grades = (
                        from grade in _database.Context.Grades
                        join studentGrade in _database.Context.StudentGrades on grade.StudentGradeId equals studentGrade.Id
                        join student in _database.Context.Students on studentGrade.StudentId equals student.Id
                        select new
                        {
                            Id = studentGrade.Id,
                            Student = student.FirstName + " " + student.LastName
                        }).ToList();

                    _mainWindow.mainDataGrid.ItemsSource = grades;

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
