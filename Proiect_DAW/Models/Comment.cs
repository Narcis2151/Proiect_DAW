using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect_DAW.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string? Text { get; set; }
        public DateTime CreateDate { get; set; }
        [ForeignKey("CreateUser")]
        public string? CreatorUserId { get; set; }
        public virtual ApplicationUser? CreatorUser { get; set; }
        public int? PostId { get; set; }
        public virtual Post? Post { get; set; }

    }
}
