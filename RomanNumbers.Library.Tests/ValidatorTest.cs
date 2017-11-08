using NUnit.Framework;
using RomanNumbers.Library.Validation;

namespace RomanNumbers.Library.Tests
{
    [TestFixture]
    public class ValidatorTest
    {
        readonly Validator _validator = new Validator();

        [TestCase("122", ErrorType.None)]
        [TestCase("1", ErrorType.None)]
        [TestCase("3999", ErrorType.None)]
        public void Validate_valid_cases(string number, ErrorType expected)
        {
            var actual = _validator.Validate(number);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("q1", ErrorType.InvalidIntegerInput)]
        [TestCase("0", ErrorType.ZeroInput)]
        [TestCase("4000", ErrorType.OutOfRangeInput)]
        public void Validate_invalid_cases(string number, ErrorType expected)
        {
            var actual = _validator.Validate(number);
            Assert.AreEqual(expected, actual);
        }
    }
}
