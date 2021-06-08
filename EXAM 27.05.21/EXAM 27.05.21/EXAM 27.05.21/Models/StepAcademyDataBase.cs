using System;
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
        private StepAcademyContext _context;
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

            _context = new StepAcademyContext(options);

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
                context.Lecturers.Load();
                context.Specialties.Load();
                context.Students.Load();
                context.StudentGrades.Load();
                context.Subjects.Load();
            }
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
    }
}
