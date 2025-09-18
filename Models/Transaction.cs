namespace CurrencyConverterAPI.Models
{
	public class Transaction
	{
		public int Id { get; set; }
		public string FromCurrency { get; set; } = string.Empty;
		public string ToCurrency { get; set; } = string.Empty;
		public decimal Amount { get; set; }
		public decimal Result { get; set; }
		public decimal Rate { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
