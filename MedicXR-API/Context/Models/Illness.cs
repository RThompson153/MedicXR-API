namespace MedicXR_API.Context.Models
{
    public class Illness
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Symptoms { get; set; }
    }
}
