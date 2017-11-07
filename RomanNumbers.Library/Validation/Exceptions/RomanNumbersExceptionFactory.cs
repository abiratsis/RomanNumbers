namespace RomanNumbers.Library.Validation.Exceptions
{
    internal class RomanNumbersExceptionFactory
    {
        public static RomanNumbersBaseException Create(ErrorType error)
        {
            switch (error)
            {
                case ErrorType.InvalidIntegerInput:
                    return new InvalidIntegerException();
                case ErrorType.OutOfRangeInput:
                    return new OutOfRangeException();
                case ErrorType.ZeroInput:
                    return new ZeroInputException();
                default:
                    return null;

            }
        }
    }
}
