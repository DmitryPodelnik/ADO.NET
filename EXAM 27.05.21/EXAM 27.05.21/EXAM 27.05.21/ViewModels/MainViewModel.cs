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
using EXAM_27._05._21.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace EXAM_27._05._21.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private string _connectionString;
        public List<string> Tables { get; set; } = new();

        private AcademyViewModel _academyViewModel;
        private AddressViewModel _addressViewModel;
        private CreditViewModel _creditViewModel;
        private GenderViewModel _genderViewModel;
        private GradeViewModel _gradeViewModel;
        private GroupDescription _groupViewModel;
        private LeaderViewModel _leaderViewModel;
        private SpecialtyViewModel _specialtyViewModel;
        private StudentGradeViewModel _studentGradeViewModel;
        private StudentViewModel _studentViewModel;
        private SubjectViewModel _subjectViewModel;

        public List<IViewItems> Items { get; set; } = new();

        public MainViewModel()
        {
            // Получаем объект конфигурации, которую берем из файла appsettings.json
            var configuration =
                new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

            // Получаем строку подключения из файла appsettings.json
            _connectionString = configuration.GetConnectionString("DefaultConnection");

            // Создаем объект контекста EF, указываем ему строку соединения и
            // получаем объект настроек для конструктора объекта контекста EF
            var options =
                new DbContextOptionsBuilder<StepAcademyContext>()
                    .UseSqlServer(_connectionString)
                    .Options;
            using (var context = new StepAcademyContext(options))
            {
                context.Academies.Load();
                context.AcademyPhones.Load();
                context.Addresses.Load();
                context.Credits.Load();
                context.Genders.Load();
                context.Grades.Load();
                context.Groups.Load();
                context.Leaders.Load();
                context.Specialties.Load();
                context.Students.Load();
                context.StudentGrades.Load();
                context.Subjects.Load();
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                // Открываем соединение
                connection.Open();

                DataTable schema = connection.GetSchema("Tables");
                foreach (DataRow row in schema.Rows)
                {
                    Tables.Add(row[2].ToString());
                }

            } // закрываем соединение

            /*
             // SQL-запросы
             ObjectContext context =
                (new StepAcademyContext() as IObjectContextAdapter).ObjectContext;

             // Создать объект подключения и команду
             SqlConnection connection = new SqlConnection(
                 @"Data Source=.\SQLEXPRESS;Initial Catalog=MyShop");

             // Создать запрос
             ObjectQuery<DbDataRecord> Customers =
                 context.CreateQuery<DbDataRecord>("SELECT c.FirstName FROM Customers AS c");

             // Отобразить имя первого покупателя в таблице Cusomers
             Console.WriteLine(Customers != null ?
                 Customers.First()["FirstName"].ToString()
                 : "Таблица пустая");
            */
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
