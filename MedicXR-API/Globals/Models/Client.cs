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
    }
}
