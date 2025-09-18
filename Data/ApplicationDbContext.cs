using Microsoft.EntityFrameworkCore;
using CurrencyConverterAPI.Models;

namespace CurrencyConverterAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ConversionHistory> ConversionHistories { get; set; }
    }
}
