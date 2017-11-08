namespace RomanNumbers.Library
{
    public interface IConverter
    {
        string ArabicNumber { get; set; }
        
        string Convert();
    }
}
