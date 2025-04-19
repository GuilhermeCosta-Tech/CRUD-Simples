using CrudClientes.ApiService.Repositories;
using CrudClientes.ApiService.Models;
using CrudClientes.ApiService.Repositories;

// Cria o builder para configurar e construir o aplicativo
var builder = WebApplication.CreateBuilder(args);

// Configuração dos serviços da aplicação
builder.Services.AddSingleton<IClienteRepository, ClienteRepository>(); // Registra o repositório de clientes como um serviço Singleton
builder.Services.AddProblemDetails(); // Adiciona suporte para respostas de detalhes de problemas (RFC 7807)
builder.AddServiceDefaults(); // Configuração padrão do Aspire (framework ou biblioteca adicional)

// Constrói o aplicativo
var app = builder.Build();

// Configuração de middlewares
app.UseExceptionHandler(); // Middleware para lidar com exceções e retornar respostas apropriadas

// ENDPOINTS da API de clientes

// Endpoint para listar todos os clientes
app.MapGet("/api/clientes", (IClienteRepository repo) =>
{
    return Results.Ok(repo.GetAllClients()); // Retorna a lista de clientes
});

// Endpoint para buscar um cliente pelo ID
app.MapGet("/api/clientes/{id}", (int id, IClienteRepository repo) =>
{
    var cliente = repo.GetById(id); // Busca o cliente pelo ID
    return cliente is not null ? Results.Ok(cliente) : Results.NotFound(); // Retorna o cliente ou um erro 404
});

// Endpoint para adicionar um novo cliente
app.MapPost("/api/clientes", (Cliente cliente, IClienteRepository repo) =>
{
    repo.Add(cliente); // Adiciona o cliente ao repositório
    return Results.Created($"/api/clientes/{cliente.Id}", cliente); // Retorna o cliente criado com o status 201
});

// Endpoint para atualizar um cliente existente
app.MapPut("/api/clientes/{id}", (int id, Cliente cliente, IClienteRepository repo) =>
{
    var existente = repo.GetById(id); // Verifica se o cliente existe
    if (existente is null)
        return Results.NotFound(); // Retorna 404 se o cliente não for encontrado

    cliente.Id = id; // Garante que o ID do cliente seja o mesmo da URL
    repo.Update(cliente); // Atualiza o cliente no repositório
    return Results.NoContent(); // Retorna 204 indicando sucesso sem conteúdo
});

// Endpoint para excluir um cliente pelo ID
app.MapDelete("/api/clientes/{id}", (int id, IClienteRepository repo) =>
{
    var existente = repo.GetById(id); // Verifica se o cliente existe
    if (existente is null)
        return Results.NotFound(); // Retorna 404 se o cliente não for encontrado

    repo.Delete(id); // Remove o cliente do repositório
    return Results.NoContent(); // Retorna 204 indicando sucesso sem conteúdo
});

// Endpoint de exemplo do Aspire (WeatherForecast)
// Gera previsões de clima fictícias
string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)), // Data da previsão
            Random.Shared.Next(-20, 55), // Temperatura em Celsius
            summaries[Random.Shared.Next(summaries.Length)] // Resumo do clima
        ))
        .ToArray();
    return forecast; // Retorna a lista de previsões
})
.WithName("GetWeatherForecast"); // Nomeia o endpoint para facilitar a identificação

// Aspire: Configuração de endpoints padrão
app.MapDefaultEndpoints();

// 🚀 Executa a aplicação
app.Run();

// Classe para representar previsões de clima
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    // Calcula a temperatura em Fahrenheit
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
