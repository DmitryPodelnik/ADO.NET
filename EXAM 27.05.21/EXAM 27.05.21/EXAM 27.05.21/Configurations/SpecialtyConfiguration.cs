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
    public class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
    {
        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.HasData(
              new Specialty[]
              {
                    new Specialty { Id = 1, SpecialtyCode = 1, Name = "Software Development"},
                    new Specialty { Id = 2, SpecialtyCode = 2, Name = "Design"},
                    new Specialty { Id = 3, SpecialtyCode = 3, Name = "System Administration"}
              });
        }
    }
}
