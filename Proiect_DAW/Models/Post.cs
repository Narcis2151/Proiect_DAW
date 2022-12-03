using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string? Text { get; set; }
        public int Likes { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
        public string? ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
    }
}
