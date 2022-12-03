using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        [Required(ErrorMessage = "Conținutul postării este obligatoriu")]
        public string PostText { get; set; }
        public int PostLikes { get; set; }
        public DateTime PostCreateDate { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
