using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EXAM_27._05._21.Models
{
    public enum Choses
    {
        YES = 6,
        NO
    }

    public class StepAcademyDataBase
    {
        static private StepAcademyContext context;
        static public StepAcademyContext Context
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

        public Choses IsConfirmed()
        {
            var result = MessageBox.Show("Are you sure to delete this item?", "Deleting item", MessageBoxButton.YesNo);

            return (Choses)result;
        }

        public async Task DeleteItem(string selectedTable, StepAcademy window)
        {
            if (IsConfirmed() == Choses.NO)
                return;



            int i = window.mainDataGrid.SelectedIndex;
            string stringItem = window.mainDataGrid.Items[i].ToString();  // this give you access to the row
            string stringId = null;

            if (selectedTable != "Students" && selectedTable != "Students'Grades" && selectedTable != "Academies' Phones" &&
                selectedTable != "Groups" && selectedTable != "Leaders" && selectedTable != "Lecturers")
                stringId = stringItem.Substring(0, stringItem.IndexOf(";"));
            else
                stringId = stringItem.Substring(7, 1);

            int id = Int32.Parse(stringId);

            switch (selectedTable)
            {
                case "Academies":

                    var deleteAcademy = await Context.Academies.FirstOrDefaultAsync(a => a.Id == id);
                    if (deleteAcademy != null)
                    {
                        Context.Academies.Remove(deleteAcademy);

                        //
                        // Сохранение изменений в БД
                        //
                        await context.SaveChangesAsync();

                        /// refresh datagrid rows
                        window.mainDataGrid.ItemsSource = await Context.Academies.ToListAsync();
                    }

                    break;

                case "Academies' Phones":

                    var deleteAcademyPhone = await Context.AcademyPhones.FirstOrDefaultAsync(a => a.Id == id);
                    if (deleteAcademyPhone != null)
                    {
                        Context.AcademyPhones.Remove(deleteAcademyPhone);

                        //
                        // Сохранение изменений в БД
                        //
                        await context.SaveChangesAsync();

                        /// refresh datagrid rows
                        GetAllAcademyPhones(ref window);
                    }

                    break;

                case "Addresses":

                    var deleteAddress = await Context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
                    if (deleteAddress != null)
                    {
                        Context.Addresses.Remove(deleteAddress);

                        //
                        // Сохранение изменений в БД
                        //
                        await context.SaveChangesAsync();

                        /// refresh datagrid rows
                        window.mainDataGrid.ItemsSource = await Context.Addresses.ToListAsync();
                    }

                    break;

                case "Records":

                    var deleteRecord = await Context.Records.FirstOrDefaultAsync(a => a.Id == id);
                    if (deleteRecord != null)
                    {
                        Context.Records.Remove(deleteRecord);

                        //
                        // Сохранение изменений в БД
                        //
                        await context.SaveChangesAsync();

                        /// refresh datagrid rows
                        window.mainDataGrid.ItemsSource = await Context.Records.ToListAsync();
                    }

                    break;

                case "Genders":

                    var deleteGender = await Context.Genders.FirstOrDefaultAsync(a => a.Id == id);
                    if (deleteGender != null)
                    {
                        Context.Genders.Remove(deleteGender);

                        //
                        // Сохранение изменений в БД
                        //
                        await context.SaveChangesAsync();

                        /// refresh datagrid rows
                        window.mainDataGrid.ItemsSource = await Context.Genders.ToListAsync();
                    }

                    break;

                case "Groups":

                    var deleteGroup = await Context.Groups.FirstOrDefaultAsync(a => a.Id == id);
                    if (deleteGroup != null)
                    {
                        Context.Groups.Remove(deleteGroup);

                        //
                        // Сохранение изменений в БД
                        //
                        await context.SaveChangesAsync();

                        /// refresh datagrid rows
                        GetAllGroups(ref window);
                    }

                    break;

                case "Leaders":

                    var deleteLeader = await Context.Leaders.FirstOrDefaultAsync(a => a.Id == id);
                    if (deleteLeader != null)
                    {
                        Context.Leaders.Remove(deleteLeader);

                        //
                        // Сохранение изменений в БД
                        //
                        await context.SaveChangesAsync();

                        /// refresh datagrid rows
                        GetAllLeaders(ref window);
                    }

                    break;

                case "Lecturers":

                    var deleteLecturer = await Context.Lecturers.FirstOrDefaultAsync(a => a.Id == id);
                    if (deleteLecturer != null)
                    {
                        Context.Lecturers.Remove(deleteLecturer);

                        //
                        // Сохранение изменений в БД
                        //
                        await context.SaveChangesAsync();

                        /// refresh datagrid rows
                        GetAllLecturers(ref window);
                    }

                    break;

                case "Specialties":

                    var deleteSpecialty = await Context.Specialties.FirstOrDefaultAsync(a => a.Id == id);
                    if (deleteSpecialty != null)
                    {
                        Context.Specialties.Remove(deleteSpecialty);

                        //
                        // Сохранение изменений в БД
                        //
                        await context.SaveChangesAsync();

                        /// refresh datagrid rows
                        window.mainDataGrid.ItemsSource = await Context.Specialties.ToListAsync();
                    }

                    break;

                case "Students":

                    var deleteStudent = await Context.Students.FirstOrDefaultAsync(a => a.Id == id);
                    if (deleteStudent != null)
                    {
                        Context.Students.Remove(deleteStudent);

                        //
                        // Сохранение изменений в БД
                        //
                        await context.SaveChangesAsync();

                        /// refresh datagrid rows
                        GetAllStudents(ref window);
                    }

                    break;

                case "Students'Grades":

                    var deleteStudentGrade = await Context.StudentGrades.FirstOrDefaultAsync(a => a.Id == id);
                    if (deleteStudentGrade != null)
                    {
                        Context.StudentGrades.Remove(deleteStudentGrade);

                        //
                        // Сохранение изменений в БД
                        //
                        await context.SaveChangesAsync();

                        /// refresh datagrid rows
                        GetAllStudentsGrades(ref window);
                    }

                    break;

                case "Subjects":

                    var deleteSubject = await Context.Subjects.FirstOrDefaultAsync(a => a.Id == id);
                    if (deleteSubject != null)
                    {
                        Context.Subjects.Remove(deleteSubject);

                        //
                        // Сохранение изменений в БД
                        //
                        await context.SaveChangesAsync();

                        /// refresh datagrid rows
                        window.mainDataGrid.ItemsSource = await Context.Subjects.ToListAsync();
                    }

                    break;
            }


        }

        static public void GetAllLecturers(ref StepAcademy window)
        {
            window.mainDataGrid.ItemsSource = Context.Lecturers
                           .LeftOuterJoin(
                                   Context.Groups,
                                   l => l.GroupId,
                                   g => g.Id,
                                   (l, g) => new
                                   {
                                       Id = l.Id,
                                       FirstName = l.FirstName,
                                       LastName = l.LastName,
                                       BirthDate = l.BirthDate,
                                       Class = g != null ? g.Name : "NULL"
                                   }).ToList();
        }

       static public void GetAllStudentsGrades(ref StepAcademy window)
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

        static public void GetAllAcademyPhones(ref StepAcademy window)
        {
            window.mainDataGrid.ItemsSource = Context.AcademyPhones
                        .LeftOuterJoin(
                                 Context.Academies,
                                 u => u.AcademyId,
                                 a => a.Id,
                                 (u, a) => new
                                 {
                                     Id = u.Id,
                                     AcademyCity = a != null ? a.City : "NULL",
                                     AcademyStreet = a != null ? a.Street : "NULL",
                                     AcademyHouse = a != null ? a.House : "NULL",
                                     Phone = u.Phone
                                 }).ToList();
        }

        static public void GetAllGroups(ref StepAcademy window)
        {
            window.mainDataGrid.ItemsSource = Context.Groups
                             .LeftOuterJoin(
                                    Context.Specialties,
                                    g => g.SpecialtyId,
                                    c => c.Id,
                                    (g, c) => new
                                    {
                                        Id = g.Id,
                                        Name = g.Name,
                                        Class = g.Class,
                                        Speciality = c != null ? c.Name : "NULL"
                                    }).ToList();
        }
        static public void GetAllLeaders(ref StepAcademy window)
        {
            window.mainDataGrid.ItemsSource = Context.Leaders
                            .LeftOuterJoin(
                                    Context.Students,
                                    l => l.StudentId,
                                    s => s.Id,
                                    (l, s) => new
                                    {
                                        Id = l.Id,
                                        Student = s != null ? s.FirstName + " " + s.LastName : "NULL",
                                        GroupId = l.GroupId
                                    }).LeftOuterJoin(
                                            Context.Groups,
                                            l => l.GroupId,
                                            g => g.Id,
                                            (l, g) => new
                                            {
                                                Id = l.Id,
                                                Learner = l.Student,
                                                Class = g != null ? g.Name : "NULL"
                                            }).ToList();
        }

        static public void GetAllStudents(ref StepAcademy window)
        {
            window.mainDataGrid.ItemsSource = Context.Students
                            .LeftOuterJoin(
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
                                        Sex = g != null ? g.Type : "NULL",
                                        AddressId = s.AddressId
                                    }).LeftOuterJoin(
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
                                                    Class = gr != null ? gr.Name : "NULL",
                                                    Sex = g.Sex,
                                                    AddressId = g.AddressId
                                                }).LeftOuterJoin(
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
                                                                Direction = addr != null ? addr.City + ", " + addr.District + ", " + addr.Street + ", " + addr.House + ", " + addr.Flat : "NULL"
                                                            }).ToList();
        }
    }
}


