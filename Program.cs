using CurrencyConverterAPI.Data;
using CurrencyConverterAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Get ConnectionString from Env Var or AppSettings
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION")
                       ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddHttpClient<IFrankfurterClient, FrankfurterClient>(client =>
{
    client.BaseAddress = new Uri("https://api.frankfurter.app/");
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Serve static files (wwwroot)
app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();

