using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RomanNumbers.Library;
using RomanNumbers.Library.Validation.Exceptions;
using System;

namespace RomanNumbers.Web.Controllers
{
    [Route("roman")]
    public class RomanConverterController : Controller
    {
        private readonly INumberConverter _numberConverter;
        private readonly ITextConverter _textConverter;
        private readonly ILogger<RomanConverterController> _logger;

        public RomanConverterController(INumberConverter converter, ITextConverter textConverter, ILogger<RomanConverterController> logger)
        {
            _numberConverter = converter;
            _textConverter = textConverter;
            _logger = logger;
        }

        [HttpGet("index")]
        public string Index()
        {
            return "Welcome to Arabic -> Roman numbers API.";
        }

        // GET roman/122
        [HttpGet("{number}")]
        public string Get(string number)
        {
            string error;
            try
            {
                _numberConverter.ArabicNumber = number;
                return _numberConverter.Convert();
            }
            catch (InvalidIntegerException)
            {
                error = "The number you have inserted is not a valid integer";
                _logger.LogError(error);
            }
            catch (OutOfRangeException)
            {
                error = "The number you have inserted is not a valid integer";
                _logger.LogError(error);
            }
            catch (ZeroInputException)
            {
                error = "Zero numbers are not allowed in roman world!";
                _logger.LogError(error);
            }
            catch (ArgumentNullException e)
            {
                error = $"Null argument for parameter {e.ParamName}";
                _logger.LogError(error);
            }
            catch (Exception e)
            {
                error = "Aknown error, please contact the system administrator.";
                _logger.LogError(e.StackTrace);
            }

            return  error;
        }

        // GET roman/text/I am a roman text 
        [HttpGet("text/{original}")]
        public IActionResult GetText(string original)
        {
            string error;
            try
            {
                _textConverter.OriginalText = original;
                return Json(_textConverter.Convert());
            }
            catch (ArgumentNullException e)
            {
                error = $"Null argument for parameter {e.ParamName}";
                _logger.LogError(error);

                return BadRequest(error);
            }
            catch (Exception e)
            {
                error = "Aknown error, please contact the system administrator.";
                _logger.LogError(e.StackTrace);

                return StatusCode(500, error);
            }
        }

    }
}
