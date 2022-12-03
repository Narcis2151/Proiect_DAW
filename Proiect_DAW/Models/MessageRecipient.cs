using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW.Models
{
    public class MessageRecipient
    {
        [Key]
        public int Id { get; set; }
        public virtual ApplicationUser? UserReceiver { get; set; }
        public virtual Group? GroupReceiver { get; set; }
        public virtual Message? Message { get; set; }

    }
}
