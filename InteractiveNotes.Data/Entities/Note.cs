using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InteractiveNotes.Data.Entities
{
    [Table("note")]
    public class Note
    {
        [Key]
        [Column("noteid")]
        public int NoteId { get; set; }

        [Required]
        [Column("title")]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        [Column("content")]
        public string Content { get; set; }

        [Column("lastmodified")]
        public DateTime CreatingDate { get; set; }
    }
}
