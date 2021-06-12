using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EXAM_27._05._21.Models
{
    [Table("Students")]
    public class Student : INotifyPropertyChanged
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [StringLength(15)]
        [Required]
        public string GradeBookNumber { get; set; }

        [StringLength(500)]
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? Note { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

        [StringLength(30)]
        [Required]
        public string Phone { get; set; }

        [StringLength(50)]
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? Email { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

        [Required]
        public short AdmissionYear { get; set; }

        // Внешние ключи.
        // Задаем правила сопоставления классов модели с таблицами БД.

        // Просто поле, используемое как внешний ключ
        public int? GroupId { get; set; }
        // Навигационное свойство
        //
        // По умолчанию для навигационного свойства использовалось бы название ****
        // в соответствии с соглашениями об именах полей в EF. Но поскольку я хочу,
        // чтобы поле, являющееся внешним ключом, называлось в таблице не ****,
        // а GroupId, то использую атрибут [ForeignKey] с нужным мне именем.
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }

        public int? GenderId { get; set; }
        [ForeignKey("GenderId")]
        public virtual Gender Gender { get; set; }

        public int? AddressId { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }

        public StudentGrade Grades { get; set; }

        public Leader Leader { get; set; }

        public override string ToString()
        {
            return Id + ";" + FirstName + ";" + LastName;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
