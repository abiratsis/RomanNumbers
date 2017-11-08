using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using RomanNumbers.Library;
using RomanNumbers.Web.Controllers;

namespace RomanNumbers.Web.Tests
{
    [TestFixture]
    public class RomanConverterControllerTest
    {
        private IConverter _converter;
        private ILogger<RomanConverterController> _logger;

        [OneTimeSetUp]
        public void Intialize()
        {
            _converter = new ArabicToRomanConverter();

            ILoggerFactory loggerFactory = new NullLoggerFactory();
            _logger = new Logger<RomanConverterController>(loggerFactory);
        }

        [Test]
        public void Should_return_III_roman_number()
        {
            var controller = new RomanConverterController(_converter, _logger);
            string actual = controller.Convert("3");

            Assert.AreEqual("I I I", actual);
        }

        [Test]
        public void Should_return_CCXXXIII_roman_number()
        {
            var controller = new RomanConverterController(_converter, _logger);

            string actual = controller.Convert("233");

            Assert.AreEqual("C C X X X I I I", actual);
        }

        [Test]
        public void Should_return_invalid_integer_message()
        {
            var controller = new RomanConverterController(_converter, _logger);

            string actual = controller.Convert("111a");

            Assert.AreEqual("The number you have inserted is not a valid integer", actual);
        }

        [Test]
        public void Should_return_out_of_range_message()
        {
            var controller = new RomanConverterController(_converter, _logger);

            string actual = controller.Convert("4000");

            Assert.AreEqual("The number you have inserted is not a valid integer", actual);
        }
    }
}
