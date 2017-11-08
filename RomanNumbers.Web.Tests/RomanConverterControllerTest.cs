using Microsoft.AspNetCore.Mvc;
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
        private INumberConverter _numberConverter;
        private ITextConverter _textConverter;
        private ILogger<RomanConverterController> _logger;

        [OneTimeSetUp]
        public void Intialize()
        {
            _numberConverter = new RomanNumberConverter();
            _textConverter = new RomanTextConverter();

            ILoggerFactory loggerFactory = new NullLoggerFactory();
            _logger = new Logger<RomanConverterController>(loggerFactory);
        }

        [Test]
        public void Get_action_should_return_III_roman_number()
        {
            var controller = new RomanConverterController(_numberConverter, _textConverter, _logger);
            string actual = controller.Get("3");

            Assert.AreEqual("I I I", actual);
        }

        [Test]
        public void Get_action_should_return_CCXXXIII_roman_number()
        {
            var controller = new RomanConverterController(_numberConverter, _textConverter, _logger);

            string actual = controller.Get("233");

            Assert.AreEqual("C C X X X I I I", actual);
        }

        [Test]
        public void Get_action_should_return_invalid_integer_message()
        {
            var controller = new RomanConverterController(_numberConverter, _textConverter, _logger);

            string actual = controller.Get("111a");

            Assert.AreEqual("The number you have inserted is not a valid integer", actual);
        }

        [Test]
        public void Get_action_should_return_out_of_range_message()
        {
            var controller = new RomanConverterController(_numberConverter, _textConverter, _logger);

            string actual = controller.Get("4000");

            Assert.AreEqual("The number you have inserted is not a valid integer", actual);
        }

        [TestCase("Artis rhetoricae partes quinque 1999 sunt: inventio, dispositio, elocutio, memoria, pronuntiatio 23", "Artis rhetoricae partes quinque M CM XC IX sunt: inventio, dispositio, elocutio, memoria, pronuntiatio X X I I I")]
        [TestCase("Artis rhetoricae partes quinque 3888 sunt: inventio, dispositio, elocutio, memoria, pronuntiatio 58", "Artis rhetoricae partes quinque M M M D C C C L X X X V I I I sunt: inventio, dispositio, elocutio, memoria, pronuntiatio L V I I I")]
        [TestCase("Artis rhetoricae partes quinque 2000 sunt: inventio, dispositio, elocutio, memoria, pronuntiatio 101", "Artis rhetoricae partes quinque M M sunt: inventio, dispositio, elocutio, memoria, pronuntiatio C I")]
        public void GetText_action_should_return_success_and_convert_all_numbers(string originalText, string expected)
        {
            var controller = new RomanConverterController(_numberConverter, _textConverter, _logger);

            IActionResult response = controller.GetText(originalText);

            Assert.IsNotNull(response);
            Assert.That(response, Is.InstanceOf<JsonResult>());

            var json = response as JsonResult;
            var result = (TextConverterResult)json?.Value;

            Assert.AreEqual(2, result.Replacements);
            Assert.AreEqual(expected, result.FinalText);
        }

        [TestCase("Artis rhetoricae partes quinque 9999 sunt: inventio, dispositio, elocutio, memoria, pronuntiatio 23", "Artis rhetoricae partes quinque 9999 sunt: inventio, dispositio, elocutio, memoria, pronuntiatio X X I I I")]
        [TestCase("Artis rhetoricae partes quinque 0 sunt: inventio, dispositio, elocutio, memoria, pronuntiatio 58", "Artis rhetoricae partes quinque 0 sunt: inventio, dispositio, elocutio, memoria, pronuntiatio L V I I I")]
        [TestCase("Artis rhetoricae partes quinque 1q sunt: inventio, dispositio, elocutio, memoria, pronuntiatio 101", "Artis rhetoricae partes quinque 1q sunt: inventio, dispositio, elocutio, memoria, pronuntiatio C I")]
        public void GetText_action_should_leave_invalid_numbers_anchanged(string originalText, string expected)
        {
            var controller = new RomanConverterController(_numberConverter, _textConverter, _logger);

            IActionResult response = controller.GetText(originalText);

            Assert.IsNotNull(response);
            Assert.That(response, Is.InstanceOf<JsonResult>());

            var json = response as JsonResult;
            var result = (TextConverterResult)json?.Value;

            Assert.AreEqual(1, result.Replacements);
            Assert.AreEqual(expected, result.FinalText);
        }

        [Test]
        public void GetText_action_null_input_should_throw_exception()
        {
            var controller = new RomanConverterController(_numberConverter, _textConverter, _logger);

            IActionResult response = controller.GetText(null);

            Assert.IsNotNull(response);
            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());

            var badRequest = response as BadRequestObjectResult;

            Assert.AreEqual(400, badRequest?.StatusCode);
            Assert.AreEqual("Null argument for parameter original", badRequest?.Value.ToString());
        }

    }
}
