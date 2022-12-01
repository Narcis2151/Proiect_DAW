using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect_DAW.Models
{
    public class Profile
    {
        [Key, ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        [Required(ErrorMessage = "Numele complet este obligatoriu")]
        public string ProfileFirstName { get; set; }
        public string ProfileLastName { get; set; }
        public DateTime ProfileBirthdate { get; set; }  
        [Required(ErrorMessage = "Starea profilului este obligatorie")]
        public bool IsPrivate { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }    

    }
}
