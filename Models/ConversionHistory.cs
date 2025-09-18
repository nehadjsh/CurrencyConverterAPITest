namespace CurrencyConverterAPI.Models
{
    public class ConversionHistory
    {
        public int Id { get; set; }
        public string FromCurrency { get; set; } = string.Empty;
        public string ToCurrency { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public decimal Result { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
