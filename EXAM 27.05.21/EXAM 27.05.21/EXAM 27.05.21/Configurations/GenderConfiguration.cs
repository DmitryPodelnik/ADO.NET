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
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.HasData(
              new Gender[]
              {
                    new Gender { Id = 1, Type = "Male"},
                    new Gender { Id = 2, Type = "Female"}
              });

            builder
            .HasMany(p => p.Students)
            .WithOne(t => t.Gender)
            .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
