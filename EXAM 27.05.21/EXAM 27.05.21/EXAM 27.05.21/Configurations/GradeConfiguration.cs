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
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.HasData(
              new Grade[]
              {
                    new Grade { Id = 1, Mark = "A", Value = 98, StudentGradeId = 1, RecordId = 1},
                    new Grade { Id = 2, Mark = "B", Value = 88, StudentGradeId = 2, RecordId = 2},
                    new Grade { Id = 3, Mark = "C", Value = 78, StudentGradeId = 3, RecordId = 3},
                    new Grade { Id = 4, Mark = "D", Value = 65, StudentGradeId = 3, RecordId = 4}
              });

            builder
           .HasOne(p => p.StudentGrade)
           .WithMany(t => t.Grades)
           .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
