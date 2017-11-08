using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
            MatchCollection matches = Regex.Matches(_originalText, @"(?:^|\s)(\d+)(?:\s|$)");
            HashSet<string> uniqueNumbers = new HashSet<string>();
            foreach (Match m in matches)
            {
                uniqueNumbers.Add(m.Groups[1].Value);
            }

            int replacements = 0;
            string resultText = _originalText;
            foreach (string n in uniqueNumbers)
            {
                try
                {
                    converter.ArabicNumber = n;
                    resultText = resultText.Replace(n, converter.Convert());
                    replacements++;
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
