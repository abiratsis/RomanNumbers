using System;
using RomanNumbers.Library;
using RomanNumbers.Library.Validation.Exceptions;

namespace NumbersToRoman
{
    class Program
    {
        // 1 -> I
        // 2 -> II
        // 4 -> IV
        // 3880 -> MMMDCCCLXXX
        // 322 -> CCCXXII
        // 21 -> XXI
        // 999 -> CM XC IX
        static void Main(string[] args)
        {
            int num = 0;

            try
            {
                var convertor = new ArabicToRomanConverter(num.ToString());
                string roman = convertor.Convert();

                Console.WriteLine($"Roman value of {num} is {roman}");
            }
            catch (RomanNumbersBaseException ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("Press any key to continue!");
            Console.ReadKey();
        }
    }
}