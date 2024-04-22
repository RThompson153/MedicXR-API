using MedicXR_API.Services.Athena.Models.Providers;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicXR_API.Globals.Models
{
	public class Client
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime? LastConnection { get; set; }
        public bool? Active { get; set; }
        public bool? Registered { get; set; }
        public string PracticeId { get; set; }
        [NotMapped]
        public IEnumerable<Provider> Providers { get; set; }
    }
}
