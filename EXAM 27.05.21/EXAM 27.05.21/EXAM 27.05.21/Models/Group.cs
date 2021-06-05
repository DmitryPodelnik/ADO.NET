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
    [Table("Groups")]
    public class Group : INotifyPropertyChanged
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [Required]
        public byte Class { get; set; }

        // Внешние ключи.
        // Задаем правила сопоставления классов модели с таблицами БД.

        // Просто поле, используемое как внешний ключ
        //public long? LecturerId { get; set; }
        // Навигационное свойство
        //
        // По умолчанию для навигационного свойства использовалось бы название ****
        // в соответствии с соглашениями об именах полей в EF. Но поскольку я хочу,
        // чтобы поле, являющееся внешним ключом, называлось в таблице не ****,
        // а LecturerId, то использую атрибут [ForeignKey] с нужным мне именем.
        //[ForeignKey("LecturerId")]
        //public virtual Lecturer Lecturer { get; set; }
        public ICollection<Lecturer> Lecturers { get; set; }

        //public int? LeaderId { get; set; }
        //[ForeignKey("LeaderId")]
        public virtual Leader Leader { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
