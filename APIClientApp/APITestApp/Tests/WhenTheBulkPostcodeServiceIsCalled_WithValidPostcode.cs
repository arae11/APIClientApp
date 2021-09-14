using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using APITestApp.Services;

namespace APITestApp.Tests
{
    public class WhenTheBulkPostcodeServiceIsCalled_WithValidPostcode
    {
        private BulkPostcodeService _bulkPostcodeService;
        [OneTimeSetUp]
        public async Task OneTimeSetupAsync()
        {
            _bulkPostcodeService = new BulkPostcodeService();
            await _bulkPostcodeService.MakeRequestAsync(new string[]{ "OX49 5NU", "M32 0JG", "NE30 1DP" });
        }

        [Test]
        public void StatusIs200()
        {
            Assert.That(_bulkPostcodeService.ResponseContent["status"].ToString(), Is.EqualTo("200"));
        }

        [Test]
        public void StatusIs200_alt()
        {
            Assert.That(_bulkPostcodeService.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void CorrectPostcodeIsReturned()
        {
            var result = _bulkPostcodeService.ResponseContent["result"][0]["result"]["postcode"].ToString();
            Assert.That(result, Is.EqualTo("OX49 5NU"));
        }

        [Test]
        public void ObjectStatusIs200()
        {
            var result = _bulkPostcodeService.ResponseObject.status;
            Assert.That(result, Is.EqualTo(200));
        }

        [Test]
        public void AdminDistrict_IsNorthTyneside()
        {
            var result = _bulkPostcodeService.ResponseContent["result"][2]["result"]["admin_district"].ToString();
            Assert.That(result, Is.EqualTo("North Tyneside"));
        }
    }
}
