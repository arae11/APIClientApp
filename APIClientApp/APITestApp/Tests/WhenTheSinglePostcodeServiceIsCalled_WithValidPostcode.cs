using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using APITestApp.Services;

namespace APITestApp.Tests
{
    public class WhenTheSinglePostcodeServiceIsCalled_WithValidPostcode
    {
        private SinglePostcodeService _singlePostcodeService;
        [OneTimeSetUp]
        public async Task OneTimeSetupAsync()
        {
            _singlePostcodeService = new SinglePostcodeService();
            await _singlePostcodeService.MakeRequestAsync("EC2Y 5AS");
        }

        [Test]
        public void StatusIs200()
        {
            Assert.That(_singlePostcodeService.ResponseContent["status"].ToString(), Is.EqualTo("200"));
        }

        [Test]
        public void StatusIs200_alt()
        {
            Assert.That(_singlePostcodeService.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void CorrectPostcodeIsReturned()
        {
            var result = _singlePostcodeService.ResponseContent["result"]["postcode"].ToString();
            Assert.That(result, Is.EqualTo("EC2Y 5AS"));
        }

        [Test]
        public void ObjectStatusIs200()
        {
            var result = _singlePostcodeService.ResponseObject.Status;
            Assert.That(result, Is.EqualTo(200));
        }

        [Test]
        public void AdminDistrict_IsCityOfLondon()
        {
            var result = _singlePostcodeService.ResponseObject.result.admin_district;
            Assert.That(result, Is.EqualTo("City of London"));
        }
    }
}
