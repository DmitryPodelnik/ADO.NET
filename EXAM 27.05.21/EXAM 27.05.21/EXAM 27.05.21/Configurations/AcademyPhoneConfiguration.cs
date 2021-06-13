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
    public class AcademyPhoneConfiguration : IEntityTypeConfiguration<AcademyPhone>
    {
        public void Configure(EntityTypeBuilder<AcademyPhone> builder)
        {
            builder.HasData(
              new AcademyPhone[]
              {
                    new AcademyPhone { Id = 1, Phone = "380123123", AcademyId = 1},
                    new AcademyPhone { Id = 2, Phone = "380123124", AcademyId = 2},
                    new AcademyPhone { Id = 3, Phone = "380123125", AcademyId = 3}
              });

            builder
           .HasOne(p => p.Academy)
           .WithMany(t => t.AcademyPhones)
           .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
