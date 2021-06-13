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
    public class LecturerConfiguration : IEntityTypeConfiguration<Lecturer>
    {
        public void Configure(EntityTypeBuilder<Lecturer> builder)
        {
            builder.HasData(
              new Lecturer[]
              {
                    new Lecturer { Id = 1, FirstName = "Dmitry", LastName = "Pupkin", BirthDate = Convert.ToDateTime("22/10/1981"), GroupId = 1},
                    new Lecturer { Id = 2, FirstName = "Vladislav", LastName = "Ivanov", BirthDate = Convert.ToDateTime("15/05/1985"), GroupId = 2},
                    new Lecturer { Id = 3, FirstName = "Ivan", LastName = "Petrov", BirthDate = Convert.ToDateTime("08/08/1991"), GroupId = 3}
              });

            builder
           .HasOne(p => p.Group)
           .WithMany(t => t.Lecturers)
           .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
