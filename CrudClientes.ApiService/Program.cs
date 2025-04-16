using CrudClientes.ApiService.Repositories;
using CrudClientes.ApiService.Models;
using CrudClientes.ApiService.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Serviços da aplicação
builder.Services.AddSingleton<IClienteRepository, ClienteRepository>();
builder.Services.AddProblemDetails();
builder.AddServiceDefaults(); // Aspire

var app = builder.Build();

// Middleware
app.UseExceptionHandler();

// ENDPOINTS da API de clientes
app.MapGet("/api/clientes", (IClienteRepository repo) =>
{
    return Results.Ok(repo.GetAll());
});

app.MapGet("/api/clientes/{id}", (int id, IClienteRepository repo) =>
{
    var cliente = repo.GetById(id);
    return cliente is not null ? Results.Ok(cliente) : Results.NotFound();
});

app.MapPost("/api/clientes", (Cliente cliente, IClienteRepository repo) =>
{
    repo.Add(cliente);
    return Results.Created($"/api/clientes/{cliente.Id}", cliente);
});

app.MapPut("/api/clientes/{id}", (int id, Cliente cliente, IClienteRepository repo) =>
{
    var existente = repo.GetById(id);
    if (existente is null)
        return Results.NotFound();

    cliente.Id = id;
    repo.Update(cliente);
    return Results.NoContent();
});

app.MapDelete("/api/clientes/{id}", (int id, IClienteRepository repo) =>
{
    var existente = repo.GetById(id);
    if (existente is null)
        return Results.NotFound();

    repo.Delete(id);
    return Results.NoContent();
});

// Endpoint do Aspire (WeatherForecast)
string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

// Aspire
app.MapDefaultEndpoints();

// 🚀 Executa a aplicação (só uma vez!)
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
