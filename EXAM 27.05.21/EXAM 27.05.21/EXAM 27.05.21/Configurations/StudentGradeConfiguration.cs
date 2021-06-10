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
    public class StudentGradeConfiguration : IEntityTypeConfiguration<StudentGrade>
    {
        public void Configure(EntityTypeBuilder<StudentGrade> builder)
        {
            builder.HasData(
              new StudentGrade[]
              {
                    new StudentGrade { Id = 1, StudentId = 1},
                    new StudentGrade { Id = 2, StudentId = 2},
                    new StudentGrade { Id = 3, StudentId = 3}
              });
        }
    }
}
