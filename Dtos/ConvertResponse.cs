namespace CurrencyConverterAPI.Dtos
{
    public class ConvertResponse
    {
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public decimal Result { get; set; }
        public DateTime RateDate { get; set; }
        public int TransactionId { get; set; }
    }
}
