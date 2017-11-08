using System;
using System.Linq;
using System.Text.RegularExpressions;
using RomanNumbers.Library.Validation.Exceptions;

namespace RomanNumbers.Library
{
    public class RomanTextConverter : ITextConverter
    {
        private string _originalText;

        public string OriginalText
        {
            get => _originalText;
            set
            {
                Validate(value);
                _originalText = value;
            }
        }

        public TextConverterResult Convert()
        {
            Validate(_originalText);

            RomanNumberConverter converter = new RomanNumberConverter();

            var numbers = Regex.Split(OriginalText, @"\D+").Where(n => n.Length > 0).ToList();

            int replacements = numbers.Count;
            string resultText = _originalText;
            var uniqueNumbers = numbers.Distinct();

            foreach (string n in uniqueNumbers)
            {
                try
                {
                    converter.ArabicNumber = n;
                    resultText = resultText.Replace(n, converter.Convert());
                }
                catch (RomanNumbersBaseException)
                {
                    //if invalid input occured ignore this number and go to the next one... so invalid numbers will not be replaced!
                }
            }

            return new TextConverterResult
            {
                FinalText = resultText,
                Replacements = replacements
            };
        }

        public void Validate(string original)
        {
            if (original == null)
                throw new ArgumentNullException(nameof(original));
        }
    }
}
