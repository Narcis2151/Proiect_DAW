using Microsoft.AspNetCore.Identity;

namespace Proiect_DAW.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual Profile Profile { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
