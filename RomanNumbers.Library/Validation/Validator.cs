namespace RomanNumbers.Library.Validation
{
    public class Validator
    {
        public ErrorType Validate(string number)
        {
            if (!int.TryParse(number, out int validInt))
                return ErrorType.InvalidIntegerInput;

            if (validInt == 0)
                return ErrorType.ZeroInput;

            if(validInt > 3999 || validInt < 1 )
                return ErrorType.OutOfRangeInput;

            return ErrorType.None;
        }
    }
}
