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
    [Table("Records")]
    public class Record : INotifyPropertyChanged
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[Required]
        //public short Grade { get; set; }

        [Required]
        public byte Coins { get; set; }
        [Required]
        public byte Course { get; set; }

        [StringLength(50)]
        [Required]
        // [Column(TypeName = "Credit")]
        public string Subject { get; set; }

        public int? StudentGradeId { get; set; }
        [ForeignKey("StudentGradeId")]
        public virtual StudentGrade StudentGrade { get; set; }

        public override string ToString()
        {
            return Id + ";" + Coins + ";" + Course + ";" + Subject;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
