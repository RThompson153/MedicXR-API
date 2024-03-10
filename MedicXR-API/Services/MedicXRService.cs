using MedicXR_API.Context;
using MedicXR_API.Context.Models;
using MedicXR_API.Services.Dtos;
using MedicXR_API.Services.Mappings;

namespace MedicXR_API.Services
{
    public class MedicXRService
    {
        private MedicXRContext _ctx;
        public MedicXRService(MedicXRContext ctx)
        {
            _ctx = ctx;
        }

        private async Task<IEnumerable<Illness>> getIllnesses() => await _ctx.GetIllnesses();

        public async Task<IEnumerable<IllnessDto>> GetIllnesses()
        {
            try
            {
                var illnesses = await getIllnesses();

                return illnesses.Select(i => i.MapToIllnessDto());
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
