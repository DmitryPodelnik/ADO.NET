using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        public MainViewModel()
        {
            // Получаем объект конфигурации, которую берем из файла appsettings.json
            var configuration =
                new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

            // Получаем строку подключения из файла appsettings.json
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            // Создаем объект контекста EF, указываем ему строку соединения и
            // получаем объект настроек для конструктора объекта контекста EF
            var options =
                new DbContextOptionsBuilder<StepAcademyContext>()
                    .UseSqlServer(connectionString)
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
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
