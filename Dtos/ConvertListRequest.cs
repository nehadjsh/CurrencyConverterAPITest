namespace CurrencyConverterAPI.Dtos
{
    public class ConvertListRequest
    {
        //  ("USD")
        public string From { get; set; } = string.Empty;

        // Transfer amount
        public decimal Amount { get; set; }

        // Target currencies list (Example): ["EUR","TRY","GBP"])
        public List<string> Targets { get; set; } = new();
    }
}
