namespace RomanNumbers.Library
{
    public interface ITextConverter
    {
        string OriginalText { get; set; }
        TextConverterResult Convert();
        void Validate(string original);
    }
}
