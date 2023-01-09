using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect_DAW.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public string? Text { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ApplicationUser? Sender { get; set; }
    }
}
