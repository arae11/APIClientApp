using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using APITestApp.Services;

namespace APITestApp.Tests
{
    public class WhenTheOutcodeServiceIsCalled_WithValidOutcode
    {
        private OutcodeService _outcodeService;
        [OneTimeSetUp]
        public async Task OneTimeSetupAsync()
        {
            _outcodeService = new OutcodeService();
            await _outcodeService.MakeRequestAsync("SL5");
        }

        [Test]
        public void StatusIs200()
        {
            Assert.That(_outcodeService.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void CorrectOutcodeIsReturned()
        {
            var result = _outcodeService.ResponseObject.result.outcode;
            Assert.That(result, Is.EqualTo("SL5"));
        }

        [Test]
        public void ObjectStatusIs200()
        {
            var result = _outcodeService.ResponseObject.status;
            Assert.That(result, Is.EqualTo(200));
        }

        [Test]
        public void AdminCounty_IsSurrey()
        {
            var result = _outcodeService.ResponseObject.result.admin_county;
            Assert.That(result, Is.EqualTo("Surrey"));
        }
    }
}
