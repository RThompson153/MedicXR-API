namespace MedicXR_API.Services.Models
{
	internal class IllnessDto
	{
		internal int Id { get; set; }
		internal string Name { get; set; }
		internal IEnumerable<SymptomDto> Symptoms { get; set; }
	}
}
