using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect_DAW.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "You can't post an empty message!")]
        public string? Text { get; set; }
        public DateTime CreateDate { get; set; }
        [ForeignKey("CreateUser")]
        public string? CreatorUserId { get; set; }
        public virtual ApplicationUser? CreatorUser { get; set; }
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public virtual Post? Post { get; set; }

    }
}
