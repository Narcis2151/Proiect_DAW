using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect_DAW.Models
{
    public class Profile
    {
        [Key, ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public DateTime profile_birthdate { get; set; }
        [Required(ErrorMessage = "Starea profilului este obligatorie")]
        public bool is_private { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }    

    }
}
