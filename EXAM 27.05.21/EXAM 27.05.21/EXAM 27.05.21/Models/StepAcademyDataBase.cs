using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EXAM_27._05._21.Models
{
    public class StepAcademyDataBase
    {
        private StepAcademyContext context;
        public StepAcademyContext Context
        {
            get => context;
            set
            {
                context = value;
            }
        }
        public string ConnectionString { get; set; }
        public DbContextOptions<StepAcademyContext> Options { get; set; }

        public StepAcademyDataBase()
        {
            var configuration =
                new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

            // Получаем строку подключения из файла appsettings.json
            ConnectionString = configuration.GetConnectionString("DefaultConnection");

            // Создаем объект контекста EF, указываем ему строку соединения и
            // получаем объект настроек для конструктора объекта контекста EF
            var options =
                new DbContextOptionsBuilder<StepAcademyContext>()
                    .UseSqlServer(ConnectionString)
                    .Options;
            Options = options;

            context = new StepAcademyContext(options);

            context.Academies.Load();
            context.AcademyPhones.Load();
            context.Addresses.Load();
            context.Records.Load();
            context.Genders.Load();
            context.Grades.Load();
            context.Groups.Load();
            context.Leaders.Load();
            context.Lecturers.Load();
            context.Specialties.Load();
            context.Students.Load();
            context.StudentGrades.Load();
            context.Subjects.Load();
        }

        public List<string> GetTables(List<string> Tables)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                // Открываем соединение
                connection.Open();

                DataTable schema = connection.GetSchema("Tables");

                foreach (DataRow row in schema.Rows)
                {
                    Tables.Add(row[2].ToString());
                }
            } // закрываем соединение

            return Tables;
        }

        public void GetAllLecturers(ref StepAcademy window)
        {
             window.mainDataGrid.ItemsSource = Context.Lecturers
                            .Join(
                                    Context.Groups,
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
        }

        public void GetAllStudentsGrades(ref StepAcademy window)
        {
            window.mainDataGrid.ItemsSource = (
                        from grade in Context.Grades
                        join studentGrade in Context.StudentGrades on grade.StudentGradeId equals studentGrade.Id
                        join student in Context.Students on studentGrade.StudentId equals student.Id
                        join record in Context.Records on grade.RecordId equals record.Id
                        select new
                        {
                            Id = studentGrade.Id,
                            Learner = student.FirstName + " " + student.LastName,
                            Evaluation = grade.Value + "/" + grade.Mark,
                            Record = record.Subject
                        }).ToList();
        }

        public void GetAllAcademyPhones(ref StepAcademy window)
        {
            window.mainDataGrid.ItemsSource = Context.AcademyPhones
                        .Join(
                                 Context.Academies,
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
        }

            public void GetAllGroups(ref StepAcademy window)
        {
            window.mainDataGrid.ItemsSource = Context.Groups
                             .Join(
                                    Context.Specialties,
                                    g => g.SpecialtyId,
                                    c => c.Id,
                                    (g, c) => new
                                    {
                                        Id = g.Id,
                                        Name = g.Name,
                                        Class = g.Class,
                                        Speciality = c.Name
                                    }).ToList();
        }
            public void GetAllLeaders(ref StepAcademy window)
        {
            window.mainDataGrid.ItemsSource = Context.Leaders
                            .Join(
                                    Context.Students,
                                    l => l.StudentId,
                                    s => s.Id,
                                    (l, s) => new
                                    {
                                        Id = l.Id,
                                        Student = s.FirstName + " " + s.LastName,
                                        GroupId = l.GroupId
                                    }).Join(
                                            Context.Groups,
                                            l => l.GroupId,
                                            g => g.Id,
                                            (l, g) => new
                                            {
                                                Id = l.Id,
                                                Learner = l.Student,
                                                Class = g.Name
                                            }).ToList();
        }

            public void GetAllStudents(ref StepAcademy window)
        {
            window.mainDataGrid.ItemsSource = Context.Students
                            .Join(
                                    Context.Genders,
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
                                                Context.Groups,
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
                                                            Context.Addresses,
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
        }
    }
}
