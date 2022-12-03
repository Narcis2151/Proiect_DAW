using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [Required(ErrorMessage = "Conținutul comentariului este obligatoriu")]
        public string CommentText { get; set; }
        public DateTime CommentCreateDate { get; set; }
        public string? ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }

    }
}
