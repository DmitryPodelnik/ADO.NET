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
    [Table("Addresses")]
    public class Address : INotifyPropertyChanged
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(30)]
        [Required]
        public string District { get; set; }

        [StringLength(30)]
        [Required]
        public string City { get; set; }

        [StringLength(30)]
        [Required]
        public string Street { get; set; }

        [StringLength(10)]
        [Required]
        public string House { get; set; }

        [StringLength(10)]
        [Required]
        public string Flat { get; set; }

        // Внешние ключи.
        // Задаем правила сопоставления классов модели с таблицами БД.

        // Просто поле, используемое как внешний ключ
        public List<Student> Students { get; set; } = new();

        public override string ToString()
        {
            return Id + ";" + City + ";" + District + ";" + Street + ";" + House + ";" + Flat;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
