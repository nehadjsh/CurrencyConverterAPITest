using Microsoft.EntityFrameworkCore;
using CurrencyConverterAPI.Models;

namespace CurrencyConverterAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        //public DbSet<Transaction> Transactions => Set<Transaction>();
        public DbSet<Transaction> Transactions { get; set; } = null!;

    }
}
