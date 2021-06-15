using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXAM_27._05._21.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EXAM_27._05._21.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasData(
              new Student[]
              {
                    new Student { Id = 1, FirstName = "Dmitry", LastName = "Petrov", BirthDate = Convert.ToDateTime("22/10/2001"),
                                  GradeBookNumber = "101001", Note = "-", Phone = "380501231231", Email = "1233@gmail.com", AdmissionYear = 2017,
                                  GroupId = 1, GenderId = 2, AddressId = 1}, /// , AddressId = 1
                    new Student { Id = 2, FirstName = "Andrew", LastName = "Ivanov", BirthDate = Convert.ToDateTime("23/05/2001"),
                                  GradeBookNumber = "101002", Note = "-", Phone = "380501231232", Email = "1234@gmail.com", AdmissionYear = 2017,
                                  GroupId = 2, GenderId = 2, AddressId = 2},
                    new Student { Id = 3, FirstName = "Ivan", LastName = "Ivanovich", BirthDate = Convert.ToDateTime("27/04/2000"),
                                  GradeBookNumber = "101003", Note = "-", Phone = "380501231233", Email = "1235@gmail.com", AdmissionYear = 2017,
                                  GroupId = 2, GenderId = 1, AddressId = 3}
              });

            // builder
            //.HasOne(p => p.Group)
            //.WithMany(t => t.Students)
            //.OnDelete(DeleteBehavior.Restrict);

            // builder
            //.HasOne(p => p.Gender)
            //.WithMany(t => t.Students)
            //.OnDelete(DeleteBehavior.Restrict);

           // builder
           //.HasOne(p => p.Address)
           //.WithMany(t => t.Students)
           //.OnDelete(DeleteBehavior.Restrict);
        }
    }
}
