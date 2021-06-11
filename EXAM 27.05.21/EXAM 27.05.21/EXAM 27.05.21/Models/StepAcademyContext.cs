using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXAM_27._05._21.Configurations;
using EXAM_27._05._21.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EXAM_27._05._21
{
    public class StepAcademyContext : DbContext
    {
        public DbSet<AcademyPhone> AcademyPhones { get; set; }
        public DbSet<Academy> Academies { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Leader> Leaders { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentGrade> StudentGrades { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        public StepAcademyContext(DbContextOptions<StepAcademyContext> options) : base(options)
        {
            // Если такая БД уже есть, то удаляем ее
            ConnectToDatabase();
        }

        private void ConnectToDatabase()
        {
            if (Database.CanConnect())
                Database.EnsureDeleted();

            // Создаем БД
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Выводим в режиме отладки запросы, отправляемые EF, в окно Output (меню Visual Studio: View -> Output).
            // Метод DbContextOptionsBuilder.LogTo был добавлен только начиная с EF Core 5.0.
            optionsBuilder.LogTo(s => System.Diagnostics.Debug.WriteLine(s));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AcademyConfiguration());
            modelBuilder.ApplyConfiguration(new AcademyPhoneConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new RecordConfiguration());
            modelBuilder.ApplyConfiguration(new GenderConfiguration());
            modelBuilder.ApplyConfiguration(new GradeConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new LeaderConfiguration());
            modelBuilder.ApplyConfiguration(new LecturerConfiguration());
            modelBuilder.ApplyConfiguration(new SpecialtyConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new StudentGradeConfiguration());
            modelBuilder.ApplyConfiguration(new SubjectConfiguration());
        }
    }
}
