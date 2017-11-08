namespace RomanNumbers.Library
{
    public interface INumberConverter
    {
        string ArabicNumber { get; set; }
        
        string Convert();

        void Validate(string arabic);
    }
}
