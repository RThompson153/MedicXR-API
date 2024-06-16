using MedicXR_API.Context;
using MedicXR_API.Globals.Models;
using MedicXR_API.Services;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Security.Cryptography;
using System.Net.Http.Headers;
using MedicXR_API.Libraries;
using MedicXR_API.Services.Athena;

namespace UnitTests
{
    public class Tests
    {
        private MedicXRContext _ctx;
        private MedicXRService _svc;
        private AthenaEMRService _athena;
        private IConfiguration _config;
        private string _clientId = "RGV2ZWxvcG1lbnQgQ2xpZW50";
        private string _clientSecret = "F7CzIZPBYmQsDeQAVDxC-Gmt61pBvvF1XH4oPzwHrib5Tsh4_0BXESyDWdXdP5bcNSVs7DREGA2oHOzaEWHSGQ";

        [SetUp]
        public void Setup()
        {
            _config = new ConfigurationBuilder().AddJsonFile("testsettings.json").AddEnvironmentVariables() 
                 .Build();
            _ctx = new MedicXRContext("Server=tcp:medicxr.database.windows.net,1433;Initial Catalog=medicxr;Persist Security Info=False;User ID=medicxr;Password=&)(^4081RPT123cj;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            _athena = new AthenaEMRService(_config, new HttpLibrary());
            _svc = new MedicXRService(_config, _athena);
        }

        [Test]
        public async Task GetClientTest()
        {
            var client = await _svc.AuthenticateClient(_clientId, _clientSecret, "192.168.0.187");

            Assert.Pass();
        }

        [Test]
        public async Task GetProvidersTest()
        {
            var providers = await _athena.GetProviders("1128700");

            var meh = providers.OrderBy(p => p.Id).Select(p => p.Id);

            Assert.Pass();
        }

        [Test]
        public async Task GetAppointmentsTest()
        {
            var appointments = await _athena.GetAppointments(1128700, 1, 2);

            Assert.Pass();
        }

        [Test]
        public async Task LoadAppointmentTest()
        {
            var practiceId = 1128700;
            var appointments = await _athena.GetAppointments(practiceId, 1, 2);

            var patient = await _athena.GetPatient(practiceId, int.Parse(appointments.FirstOrDefault().PatientId));

            Assert.Pass();
        }

        [Test]
        public async Task GetConditionsTest()
        {
            await _athena.GetConditions();

            Assert.Pass();
        }        

        [Test]
        public void createapikey()
        {
            var rng = RandomNumberGenerator.Create();
            var bytes = new byte[64];

            rng.GetBytes(bytes);

            var key = Convert.ToBase64String(bytes).Replace('+', '-').Replace('/', '_');
            Assert.Pass();
        }

         [Test]
        public void createclientkey()
        {
            var rng = RandomNumberGenerator.Create();
            var bytes = new byte[128];

            rng.GetBytes(bytes);

            var key = Convert.ToBase64String(bytes);
            Assert.Pass();
        }
    }
}