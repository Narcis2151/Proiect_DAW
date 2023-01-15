namespace Proiect_DAW.Models
{
    public class Intermediar
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public IDictionary<string, bool> Users { get; set; }
    }
}
