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
    [Table("Grades")]
    public class Grade : INotifyPropertyChanged
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(10)]
        [Required]
        public string Mark { get; set; }

        [Required]
        // [Column(TypeName = "Grade")]
        public short Value { get; set; }

        public int? StudentGradeId { get; set; }
        [ForeignKey("StudentGradeId")]
        public virtual StudentGrade StudentGrade { get; set; }

        public int? RecordId { get; set; }
        [ForeignKey("RecordId")]
        public virtual Record Record { get; set; }

        public override string ToString()
        {
            return Id + ";" + Mark + ";" + Value + ";" + StudentGradeId + ";" + RecordId;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
