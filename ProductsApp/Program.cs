using Microsoft.EntityFrameworkCore;
using ProductsApp.Data;
using ProductsApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProductContext>();

    const int maxRetries = 20;
    var delay = TimeSpan.FromSeconds(3);

    for (int attempt = 1; ; attempt++)
    {
        try
        {
            Console.WriteLine($"[DB] Migrate attempt {attempt}/{maxRetries} ...");
            await db.Database.MigrateAsync();

            if (!await db.Products.AnyAsync())
            {
                db.Products.AddRange(
                    new Product { Name = "Coca-cola", Price = 10 },
                    new Product { Name = "Fanta", Price = 10 },
                    new Product { Name = "Pepsi", Price = 5 }
                );
                await db.SaveChangesAsync();
            }

            Console.WriteLine("[DB] Migration + seed OK.");
            break;
        }
        catch (Exception ex) when (attempt < maxRetries)
        {
            Console.WriteLine($"[DB] Not ready: {ex.Message}");
            await Task.Delay(delay);
        }
    }
}

app.MapGet("/products", async (ProductContext db) => await db.Products.ToListAsync());

app.MapPost("/products", async (ProductContext db, Product input) =>
{
    db.Products.Add(input);
    await db.SaveChangesAsync();
    return Results.Created($"/products/{input.Id}", input);
});


app.Run();
