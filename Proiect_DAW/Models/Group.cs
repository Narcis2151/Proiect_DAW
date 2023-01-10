using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect_DAW.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Group name is mandatory!")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual ICollection<ApplicationUserGroup>? ApplicationUserGroups { get; set; }
        [InverseProperty("Group")]
        public virtual ICollection<Message>? Messages { get; set; }

    }
}
