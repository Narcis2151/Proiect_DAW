using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW.Models
{
    public class Friendship
    {
        [Key]
        public int Id { get; set; }
        public virtual ApplicationUser? Requester { get; set; }
        public virtual ApplicationUser? Adresee { get; set; }
        public string? Status { get; set; }
    }
}
