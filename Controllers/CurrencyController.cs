using CurrencyConverterAPI.Data;
using CurrencyConverterAPI.Dtos;
using CurrencyConverterAPI.Models;
using CurrencyConverterAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace CurrencyConverterAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly IFrankfurterClient _client;
        private readonly AppDbContext _db;
        private static readonly Regex Code = new("^[A-Z]{3}$", RegexOptions.Compiled);

        public CurrencyController(IFrankfurterClient client, AppDbContext db)
        {
            _client = client;
            _db = db;
        }

        // GET: /api/currency/currencies
        [HttpGet("currencies")]
        public async Task<ActionResult<Dictionary<string, string>>> GetCurrencies(CancellationToken ct)
            => Ok(await _client.GetCurrenciesAsync(ct));

        // GET: /api/currency/history?take=50
        [HttpGet("history")]
        public async Task<ActionResult<IEnumerable<Transaction>>> History([FromQuery] int take = 50, CancellationToken ct = default)
        {
            take = Math.Clamp(take, 1, 500);
            var rows = await _db.Transactions
                .OrderByDescending(t => t.Id)
                .Take(take)
                .ToListAsync(ct);

            return Ok(rows);
        }

        // POST: /api/currency/convert
        [HttpPost("convert")]
        public async Task<ActionResult<ConvertResponse>> Convert([FromBody] ConvertRequest req, CancellationToken ct)
        {
            if (req == null) return BadRequest("Body required");
            var from = (req.From ?? "").ToUpperInvariant();
            var to = (req.To ?? "").ToUpperInvariant();
            if (!Code.IsMatch(from) || !Code.IsMatch(to)) return BadRequest("Invalid currency codes");
            if (req.Amount <= 0) return BadRequest("Amount must be > 0");

            decimal rate = await _client.GetRateAsync(from, to, ct);
            decimal result = Math.Round(req.Amount * rate, 4, MidpointRounding.AwayFromZero);

            var trx = new Transaction
            {
                FromCurrency = from,
                ToCurrency = to,
                Amount = req.Amount,
                Rate = rate,
                Result = result
            };

            _db.Transactions.Add(trx);
            await _db.SaveChangesAsync(ct);

            return Ok(new ConvertResponse
            {
                From = from,
                To = to,
                Amount = req.Amount,
                Rate = rate,
                Result = result,
                RateDate = DateTime.UtcNow,
                TransactionId = trx.Id
            });
        }

        // GET: /api/currency/convert-list?base=USD&amount=100&targets=EUR,TRY
        [HttpGet("convert-list")]
        public async Task<ActionResult<object>> ConvertList([FromQuery] string @base, [FromQuery] decimal amount, [FromQuery] string targets, CancellationToken ct = default)
        {
            if (string.IsNullOrEmpty(@base) || amount <= 0 || string.IsNullOrEmpty(targets))
                return BadRequest("Invalid parameters");

            var targetList = targets.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                    .Select(t => t.Trim().ToUpperInvariant())
                                    .Where(t => Code.IsMatch(t))
                                    .ToList();

            if (!targetList.Any())
                return BadRequest("No valid target currencies");

            var rates = new Dictionary<string, decimal>();
            foreach (var target in targetList)
            {
                if (target == @base.ToUpperInvariant()) continue;
                decimal rate = await _client.GetRateAsync(@base, target, ct);
                rates[target] = Math.Round(amount * rate, 4, MidpointRounding.AwayFromZero);
            }

            return Ok(new
            {
                baseCurrency = @base.ToUpperInvariant(),
                amount,
                rates
            });
        }
    }
}

