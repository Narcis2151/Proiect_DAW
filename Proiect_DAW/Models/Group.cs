using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect_DAW.Models
{
    public class Group
    {
        public Group() { this.ApplicationUsers = new HashSet<ApplicationUser>(); }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual ICollection<ApplicationUser>? ApplicationUsers { get; set; }
        [InverseProperty("GroupReceiver")]
        public virtual ICollection<MessageRecipient>? MessageRecipients { get; set; }

    }
}
