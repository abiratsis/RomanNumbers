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
        private readonly INumberConverter _converter;
        private readonly ILogger<RomanConverterController> _logger;

        public RomanConverterController(INumberConverter converter, ILogger<RomanConverterController> logger)
        {
            _converter = converter;
            _logger = logger;
        }

        [HttpGet("index")]
        public string Index()
        {
            return "Welcome to Arabic -> Roman numbers API.";
        }

        // GET roman/convert/122
        [HttpGet("convert/{number}")]
        public string Convert(string number)
        {
            string error;
            try
            {
                _converter.ArabicNumber = number;
                return _converter.Convert();
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

            return error;
        }


    }
}
