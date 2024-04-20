using MedicXR_API.Context.Models;
using MedicXR_API.Services.Models;

namespace MedicXR_API.Services.Utils
{
    internal static class Mappings
    {
        internal static IllnessDto MapToIllnessDto(this Illness source) => new()
        {
            Id = source.IllnessId,
            Name = source.IllnessName
        };
    }
}
