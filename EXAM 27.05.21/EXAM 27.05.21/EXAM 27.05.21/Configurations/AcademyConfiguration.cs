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
    public class AcademyConfiguration : IEntityTypeConfiguration<Academy>
    {
        public void Configure(EntityTypeBuilder<Academy> builder)
        {
            builder.HasData(
                new Academy[]
                {
                    new Academy { Id = 1, City = "Odesa", Street = "Central", House = "2A"},
                    new Academy { Id = 2, City = "Kyiv", Street = "Central", House = "5B"},
                    new Academy { Id = 3, City = "Lviv", Street = "Central", House = "3C"}
                });
        }
    }
}
