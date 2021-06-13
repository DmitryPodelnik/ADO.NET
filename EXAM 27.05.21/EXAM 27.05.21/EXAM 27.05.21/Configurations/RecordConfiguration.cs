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
    public class RecordConfiguration : IEntityTypeConfiguration<Record>
    {
        public void Configure(EntityTypeBuilder<Record> builder)
        {
            builder.HasData(
              new Record[]
              {
                    new Record { Id = 1, Subject = "Math", Coins = 20, Course = 1},
                    new Record { Id = 2, Subject = "C++", Coins = 15, Course = 2},
                    new Record { Id = 3, Subject = "C#", Coins = 40, Course = 3},
                    new Record { Id = 4, Subject = "JavaScript", Coins = 40, Course = 3}
              });

            builder
            .HasOne(p => p.StudentGrade)
            .WithMany(t => t.Records)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
