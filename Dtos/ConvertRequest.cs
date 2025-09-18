namespace CurrencyConverterAPI.Dtos
{
	public class ConvertRequest
	{
		public string From { get; set; } = string.Empty; // Example: "USD"
        public string To { get; set; } = string.Empty;   // Example: "TRY"
        public decimal Amount { get; set; }
	}
}
