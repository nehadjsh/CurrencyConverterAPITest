namespace CurrencyConverterAPI.Dtos
{
    public class ConvertListResponse
    {
        public string From { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime RateDate { get; set; }
        public List<QuoteItem> Quotes { get; set; } = new();
    }

    public class QuoteItem
    {
        public string To { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public decimal Result { get; set; }
        public int TransactionId { get; set; }
    }
}
