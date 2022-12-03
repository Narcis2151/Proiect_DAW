using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect_DAW.Models
{
    public class Profile
    {
        [Key, ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public DateTime ProfileBirthdate { get; set; }  
        public bool IsPrivate { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }    

    }
}
