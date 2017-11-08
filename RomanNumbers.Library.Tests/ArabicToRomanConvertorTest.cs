using System;
using NUnit.Framework;
using RomanNumbers.Library.Validation.Exceptions;
using Assert = NUnit.Framework.Assert;

namespace RomanNumbers.Library.Tests
{
    [TestFixture]
    public class ArabicToRomanConvertorTest
    {

        [TestCase("1", "I")]
        [TestCase("2", "I I")]
        [TestCase("3", "I I I")]
        public void Should_transform_successfully_from_1_to_3(string arabic, string expected)
        {
            string actual = new ArabicToRomanConverter(arabic).Convert();
            Assert.AreEqual(expected, actual);
        }

        [TestCase("1001", "M I")]
        [TestCase("1002", "M I I")]
        [TestCase("1003", "M I I I")]
        public void Should_transform_successfully_from_1001_to_1003(string arabic, string expected)
        {
            string actual = new ArabicToRomanConverter(arabic).Convert();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Should_transform_successfully_3880()
        {
            string actual = new ArabicToRomanConverter("3880").Convert();
            Assert.AreEqual("M M M D C C C L X X X", actual);
        }

        [Test]
        public void Should_transform_successfully_999()
        {
            string actual = new ArabicToRomanConverter("999").Convert();
            Assert.AreEqual("CM XC IX", actual);
        }

        [Test]
        public void Should_throw_exception_when_zero_input()
        {
            var ex = Assert.Throws<ZeroInputException>(() => new ArabicToRomanConverter("0"));

            Assert.That(ex, Is.InstanceOf<RomanNumbersBaseException>());
        }

        [Test]
        public void Should_throw_exception_when_out_of_range_input()
        {
            var ex = Assert.Throws<OutOfRangeException>(() => new ArabicToRomanConverter("4000"));

            Assert.That(ex, Is.InstanceOf<RomanNumbersBaseException>());
        }

        [Test]
        public void Should_throw_exception_when_invalid_integer_input()
        {
            var ex = Assert.Throws<InvalidIntegerException>(() => new ArabicToRomanConverter("1a"));

            Assert.That(ex, Is.InstanceOf<RomanNumbersBaseException>());
        }

        [Test]
        public void Should_throw_exception_when_null_input()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new ArabicToRomanConverter(null));

            Assert.That(ex.ParamName, Is.EqualTo("arabicNumber"));
        }
    }
}
