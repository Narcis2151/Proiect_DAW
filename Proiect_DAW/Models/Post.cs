using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect_DAW.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "You can't post an empty message!")]
        public string? Text { get; set; }
        public int Likes { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual List<Comment>? Comments { get; set; }
        public string? ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
    }
}