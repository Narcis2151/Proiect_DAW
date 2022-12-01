namespace Proiect_DAW.Models
{
    public class Group
    {
        public Group() { this.ApplicationUsers = new HashSet<ApplicationUser>(); }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public DateTime GroupCreateDate { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
