using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EXAM_27._05._21
{
    class StepAcademyContext : DbContext
    {
        public StepAcademyContext(DbContextOptions<StepAcademyContext> options) : base(options)
        {
            // Если такая БД уже есть, то удаляем ее
            if (Database.CanConnect())
                Database.EnsureDeleted();

            // Создаем БД
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
