﻿using MedicXR_API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MedicXR_API.Controllers
{
    [ApiController]
    [Route("/")]
    public class MedicXRController : ControllerBase
    {
        private MedicXRService _svc;
        public MedicXRController(MedicXRService svc)
        {
            _svc = svc;
        }

        [Route("getillnesses")]
        [HttpGet]
        public async Task<string> GetIllnesses()
        {
            try
            {
                return JsonSerializer.Serialize(await _svc.GetIllnesses());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);

                return ex.InnerException?.Message ?? ex.Message;
            }
        }
    }
}
