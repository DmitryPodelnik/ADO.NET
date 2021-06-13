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
    public class LeaderConfiguration : IEntityTypeConfiguration<Leader>
    {
        public void Configure(EntityTypeBuilder<Leader> builder)
        {
            builder.HasData(
              new Leader[]
              {
                    new Leader { Id = 1, StudentId = 1, GroupId = 1},
                    new Leader { Id = 2, StudentId = 2, GroupId = 2},
                    new Leader { Id = 3, StudentId = 3, GroupId = 3}
              });

            builder
           .HasOne(p => p.Group)
           .WithOne(t => t.Leader)
           .OnDelete(DeleteBehavior.Restrict);

            builder
           .HasOne(p => p.Student)
           .WithOne(t => t.Leader)
           .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
