using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoListApp.Business.Interfaces;
using TodoListApp.Business.Services;
using TodoListApp.DataAccess.Context;
using TodoListApp.DataAccess.Interfaces;
using TodoListApp.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Veritaban� ba�lant� dizesi ve migrations assembly tan�m�
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

// Repository ve service ba��ml�l�klar�
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Geli�tirme ortam�nda Swagger ve otomatik veritaban� migrate i�lemi
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
            logger.LogError(ex, "Veritaban� migrate edilirken bir hata olu�tu.");
        }
    }
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
