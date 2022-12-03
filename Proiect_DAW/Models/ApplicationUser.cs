using Microsoft.AspNetCore.Identity;

namespace Proiect_DAW.Models
{
    public class ApplicationUser : IdentityUser
    {
        //public ApplicationUser() { this.Groups = new HashSet<Group>(); }
        public virtual Profile? Profile { get; set; }
        public virtual ICollection<Post>? Posts { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
        public virtual ICollection<Group>? Groups { get; set; }
        //public virtual ICollection<Message>? Messages { get; set; }

    }
}
