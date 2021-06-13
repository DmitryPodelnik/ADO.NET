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
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasData(
              new Group[]
              {
                    new Group { Id = 1, Name = "КН-П-181", Class = 3, SpecialtyId = 1}, 
                    new Group { Id = 2, Name = "КН-П-191", Class = 2, SpecialtyId = 2},
                    new Group { Id = 3, Name = "КН-П-182", Class = 3, SpecialtyId = 3}
              });

            builder
           .HasOne(p => p.Specialty)
           .WithMany(t => t.Groups)
           .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
