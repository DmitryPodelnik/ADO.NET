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
    public class CreditConfiguration : IEntityTypeConfiguration<Credit>
    {
        public void Configure(EntityTypeBuilder<Credit> builder)
        {
            builder.HasData(
              new Credit[]
              {
                    new Credit { Id = 1, Grade = 5, Subject = "Math"},
                    new Credit { Id = 2, Grade = 4, Subject = "C++"},
                    new Credit { Id = 3, Grade = 3, Subject = "C#"}
              });
        }
    }
}
