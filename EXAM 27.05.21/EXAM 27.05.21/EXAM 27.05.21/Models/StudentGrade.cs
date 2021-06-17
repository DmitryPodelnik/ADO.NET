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
    [Table("Students'Grades")]
    public class StudentGrade : INotifyPropertyChanged
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Внешние ключи.
        // Задаем правила сопоставления классов модели с таблицами БД.

        // Просто поле, используемое как внешний ключ
        public int? StudentId { get; set; }
        // Навигационное свойство
        //
        // По умолчанию для навигационного свойства использовалось бы название ****
        // в соответствии с соглашениями об именах полей в EF. Но поскольку я хочу,
        // чтобы поле, являющееся внешним ключом, называлось в таблице не ****,
        // а StudentId, то использую атрибут [ForeignKey] с нужным мне именем.
        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        //public List<Student> Students { get; set; } = new();
        public List<Grade> Grades { get; set; } = new();
        public List<Record> Records { get; set; } = new();

        public override string ToString()
        {
            return Id + ";" + StudentId;
            //return Id.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
