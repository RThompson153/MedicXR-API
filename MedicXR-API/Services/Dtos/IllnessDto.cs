namespace MedicXR_API.Services.Dtos
{
    public class IllnessDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public IEnumerable<string> Symptoms { get; set; }
    }
}
