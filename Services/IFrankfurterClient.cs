namespace CurrencyConverterAPI.Services
{
    public interface IFrankfurterClient
    {
        Task<decimal> GetRateAsync(string from, string to, CancellationToken ct = default);
        Task<Dictionary<string, string>> GetCurrenciesAsync(CancellationToken ct = default);
    }
}
