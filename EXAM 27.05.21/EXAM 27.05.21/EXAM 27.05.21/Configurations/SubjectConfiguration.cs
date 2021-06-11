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
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasData(
              new Subject[]
              {
                    new Subject { Id = 1, Name = "Math", Course = 1, Hours = 35},
                    new Subject { Id = 2, Name = "C++", Course = 2, Hours = 35},
                    new Subject { Id = 3, Name = "C#", Course = 3, Hours = 35},
                    new Subject { Id = 4, Name = "JavaScript", Course = 3, Hours = 35},
                    new Subject { Id = 5, Name = "PHP", Course = 4, Hours = 35},
              });
        }
    }
}
