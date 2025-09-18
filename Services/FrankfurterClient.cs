using System.Net.Http.Json;

namespace CurrencyConverterAPI.Services
{
    public class FrankfurterClient : IFrankfurterClient
    {
        private readonly HttpClient _http;

        public FrankfurterClient(HttpClient http) => _http = http;

        public async Task<decimal> GetRateAsync(string from, string to, CancellationToken ct = default)
        {
            var url = $"latest?base={from}&symbols={to}";
            var data = await _http.GetFromJsonAsync<LatestRatesResponse>(url, ct)
                       ?? throw new InvalidOperationException("No data from Frankfurter");

            if (!data.Rates.TryGetValue(to, out var rate))
                throw new InvalidOperationException($"Rate {from}->{to} not found");

            return rate;
        }

        public async Task<Dictionary<string, string>> GetCurrenciesAsync(CancellationToken ct = default)
        {
            var dict = await _http.GetFromJsonAsync<Dictionary<string, string>>("currencies", ct);
            return dict ?? new();
        }
    }
}

