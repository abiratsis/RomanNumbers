using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RomanNumbers.Library.Validation;
using RomanNumbers.Library.Validation.Exceptions;

namespace RomanNumbers.Library
{
    public class ArabicToRomanConverter : IConverter
    {
        private static readonly Dictionary<int, string> _romanDictionary = new Dictionary<int, string>
        {
            {1000,"M"},
            {900,"CM"},
            {500,"D"},
            {400,"CD"},
            {100,"C"},
            {90,"XC"},
            {50,"L"},
            {40,"XL"},
            {10,"X"},
            {9,"IX"},
            {5,"V"},
            {4,"IV"},
            {1 ,"I"}
        };

        private string _arabicNumber;
        public string ArabicNumber
        {
            get => _arabicNumber;
            set
            {
                Validate(value);
                _arabicNumber = value;
            }
        }
        private static readonly Validator _validator = new Validator();

        public ArabicToRomanConverter()
        {

        }

        public ArabicToRomanConverter(string arabic)
        {
            Validate(arabic);
            _arabicNumber = arabic;
        }

        public string Convert()
        {
            StringBuilder romanNumber = new StringBuilder();
            bool moreThanOne = false;

            int arabicInt = Int32.Parse(_arabicNumber);
            var keys = _romanDictionary.Keys.Where(k => arabicInt >= k).ToList();
            for (int i = 0; i < keys.Count && arabicInt > 0; i++)
            {
                int ckey = keys[i];
                int division = arabicInt / ckey;
                if (division != 0)
                {
                    for (int j = 0; j < division; j++)
                    {
                        if (moreThanOne)
                            romanNumber.Append(' ');

                        romanNumber.Append(_romanDictionary[ckey]);
                        arabicInt -= ckey;
                        moreThanOne = true;
                    }
                }
            }

            return romanNumber.ToString();
        }

        private void Validate(string arabicNumber)
        {
            if (arabicNumber == null)
                throw new ArgumentNullException(nameof(arabicNumber));

            ErrorType result = _validator.Validate(arabicNumber);

            if(result != ErrorType.None)
                throw RomanNumbersExceptionFactory.Create(result);
        }
    }
}
