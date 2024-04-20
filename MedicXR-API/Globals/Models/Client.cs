using System.ComponentModel.DataAnnotations.Schema;

namespace MedicXR_API.Globals.Models
{
    public class Client
    {
        public string Id { get; set; }
        public string ClientName { get; set; }
        public string Location { get; set; }
        public DateTime? LastConnection { get; set; }
        public bool? Active { get; set; }
        public bool? Registered { get; set; }
        public string PracticeName { get; set; }
        public string EmrId { get; set; }
        public string UserIds { get; set; }
        [NotMapped]public IEnumerable<User>? Users { get; set; }
    }
}
