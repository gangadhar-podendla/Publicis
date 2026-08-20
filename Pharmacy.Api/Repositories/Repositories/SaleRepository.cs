using System.Text.Json;
using Pharmacy.Api.Models;
using Pharmacy.Api.Repositories.Interfaces;

namespace Pharmacy.Api.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _jsonOptions;

    public SaleRepository(IWebHostEnvironment environment)
    {
        _filePath = Path.Combine(
            environment.ContentRootPath,
            "Data",
            "sales.json");

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };
    }

    public async Task<IEnumerable<Sale>> GetAllAsync()
    {
        if (!File.Exists(_filePath))
        {
            return [];
        }

        var json = await File.ReadAllTextAsync(_filePath);

        if (string.IsNullOrWhiteSpace(json))
        {
            return [];
        }

        return JsonSerializer.Deserialize<List<Sale>>(
                   json,
                   _jsonOptions)
               ?? [];
    }

    public async Task<Sale> AddAsync(Sale sale)
    {
        var sales = (await GetAllAsync()).ToList();

        sale.Id = sales.Count == 0
            ? 1
            : sales.Max(s => s.Id) + 1;

        sales.Add(sale);

        var json = JsonSerializer.Serialize(
            sales,
            _jsonOptions);

        await File.WriteAllTextAsync(_filePath, json);

        return sale;
    }
}