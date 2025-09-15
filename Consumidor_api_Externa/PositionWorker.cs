using Domain.Entities;
using Infra.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.Json;

public class PositionWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly HttpClient _httpClient;

    public PositionWorker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("X-Test-Key", "9MsyhgyioqtMLUiUFRNm");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // escopo para o DbContext
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // aplica migrations
        await db.Database.MigrateAsync(stoppingToken);

        // chamada API
        var response = await _httpClient.GetAsync(
            "https://api.andbank.com.br/candidate/positions",
            HttpCompletionOption.ResponseHeadersRead,
            stoppingToken
        );

        response.EnsureSuccessStatusCode();

        using var stream = await response.Content.ReadAsStreamAsync(stoppingToken);
        var positions = await JsonSerializer.DeserializeAsync<List<Position>>(
            stream,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true },
            stoppingToken
        );

        // processamento em lotes
        const int batchSize = 1000;
        if (positions != null)
        {
            for (int i = 0; i < positions.Count; i += batchSize)
            {
                var batch = positions.Skip(i).Take(batchSize).ToList();
                db.Positions.AddRange(batch);
                await db.SaveChangesAsync(stoppingToken);
            }
        }

        Console.WriteLine("Data importada com sucesso.");
    }
}
