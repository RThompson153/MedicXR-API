using MedicXR_API.Context.Models;
using MedicXR_API.Services.Dtos;

namespace MedicXR_API.Services.Mappings
{
    internal static class Mappings
    {
        internal static IllnessDto MapToIllnessDto(this Illness source) => new()
        {
            Id = source.Id,
            Name = source.Name,
            Description = source.Description,
            Symptoms = source.Symptoms?.Split(',')
        };
    }
}
