using System;
using NUnit.Framework;

namespace RomanNumbers.Library.Tests
{
    [TestFixture]
    public class RomanTextConverterTest
    {
        private ITextConverter _converter;

        [OneTimeSetUp]
        public void Intialize()
        {
            _converter = new RomanTextConverter();
        }

        [TestCase("Lorem ipsum 2 dolor sit amet", "Lorem ipsum I I dolor sit amet")]
        [TestCase("Consectetur 5 adipiscing elit 9", "Consectetur V adipiscing elit IX")]
        [TestCase("Ut enim quis nostrum 1904 qui", "Ut enim quis nostrum M CM IV qui")]
        public void Should_replace_all_assignment_examples(string originalText, string expected)
        {
            _converter.OriginalText = originalText;
            var actual = _converter.Convert();

            Assert.AreEqual(expected, actual.FinalText);
        }

        [TestCase("Lorem ipsum 1001 dolor sit amet", "Lorem ipsum M I dolor sit amet")]
        [TestCase("Consectetur 1002 adipiscing elit 9", "Consectetur M I I adipiscing elit IX")]
        [TestCase("Ut enim quis nostrum 1003 qui", "Ut enim quis nostrum M I I I qui")]
        [TestCase("Lorem ipsum 3880 dolor sit amet", "Lorem ipsum M M M D C C C L X X X dolor sit amet")]
        [TestCase("Consectetur 999 adipiscing elit 9", "Consectetur CM XC IX adipiscing elit IX")]
        public void Should_replace_all_mine_examples(string originalText, string expected)
        {
            _converter.OriginalText = originalText;
            var actual = _converter.Convert();

            Assert.AreEqual(expected, actual.FinalText);
        }

        [Test]
        public void Should_throw_exception_if_original_text_is_null()
        {
            var textConverter = new RomanTextConverter();
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                textConverter.OriginalText = null;
            });

            Assert.That(ex.ParamName, Is.EqualTo("original"));
        }

        [TestCase("Lorem ipsum 4002 dolor sit amet", "Lorem ipsum 4002 dolor sit amet")]
        [TestCase("Consectetur 10000 adipiscing elit 9", "Consectetur 10000 adipiscing elit IX")]
        [TestCase("Ut enim quis nostrum 0 qui", "Ut enim quis nostrum 0 qui")]
        [TestCase("Lorem ipsum 5000 dolor sit amet", "Lorem ipsum 5000 dolor sit amet")]
        [TestCase("Consectetur 999 adipiscing elit 0", "Consectetur CM XC IX adipiscing elit 0")]
        public void Should_ignore_invalid_numbers_on_text(string originalText, string expected)
        {
            _converter.OriginalText = originalText;
            var actual = _converter.Convert();

            Assert.AreEqual(expected, actual.FinalText);
        }
    }
}
