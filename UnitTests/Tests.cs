using MedicXR_API.Context;
using MedicXR_API.Services;
using NUnit.Framework;

namespace UnitTests
{
    public class Tests
    {
        private MedicXRContext _ctx;
        private MedicXRService _svc;
        [SetUp]
        public void Setup()
        {
            _ctx = new MedicXRContext("Server=tcp:medicxr.database.windows.net,1433;Initial Catalog=medicxr;Persist Security Info=False;User ID=medicxr;Password=&)(^4081RPT123cj;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            _svc = new MedicXRService(_ctx);
        }

        [Test]
        public async Task GetIllnessesTest()
        {
            await _svc.GetIllnesses();

            Assert.Pass();
        }
    }
}