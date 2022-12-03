namespace Proiect_DAW.Models
{
    public class ApplicationUserGroup
    {
        public string? ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public int? GroupId { get; set; }
        public virtual Group? Group { get; set; }

    }
}
