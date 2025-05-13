using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoListApp.Business.Interfaces;
using TodoListApp.Business.Services;
using TodoListApp.DataAccess.Context;
using TodoListApp.DataAccess.Interfaces;
using TodoListApp.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Veritabaný baðlantý dizesi ve migrations assembly tanýmý
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var migrationsAssembly = typeof(AppDbContext).Assembly.GetName().Name;

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString, sqlOptions =>
    {
        if (migrationsAssembly != null)
        {
            sqlOptions.MigrationsAssembly(migrationsAssembly);
        }
    })
);

// Repository ve service baðýmlýlýklarý
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Geliþtirme ortamýnda Swagger ve otomatik veritabaný migrate iþlemi
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Veritabaný migrate edilirken bir hata oluþtu.");
        }
    }
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
