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
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasData(
              new Address[]
              {
                    new Address { Id = 1, District = "Kyivsky", City = "Odesa", Street = "Central", House = "1F", Flat = "23"}, // StudentId = 1
                    new Address { Id = 2, District = "Solomensky", City = "Kyiv", Street = "Central", House = "2C", Flat = "55"},
                    new Address { Id = 3, District = "Galician", City = "Lviv", Street = "Central", House = "3B", Flat = "12"}
              });

            //builder
            //.HasMany(p => p.Students)
            //.WithOne(t => t.Address)
            //.OnDelete(DeleteBehavior.Restrict);
        }
    }
}
