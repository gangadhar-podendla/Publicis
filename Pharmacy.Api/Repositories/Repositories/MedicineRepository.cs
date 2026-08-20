using System.Text.Json;
using Pharmacy.Api.Models;
using Pharmacy.Api.Repositories.Interfaces;

namespace Pharmacy.Api.Repositories;

public class MedicineRepository : IMedicineRepository
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _jsonOptions;

    public MedicineRepository(IWebHostEnvironment environment)
    {
        _filePath = Path.Combine(
            environment.ContentRootPath,
            "Data",
            "medicines.json");

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };
    }

    public async Task<IEnumerable<Medicine>> GetAllAsync()
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

        return JsonSerializer.Deserialize<List<Medicine>>(
                   json,
                   _jsonOptions)
               ?? [];
    }

    public async Task<Medicine?> GetByIdAsync(int id)
    {
        var medicines = await GetAllAsync();

        return medicines.FirstOrDefault(m => m.Id == id);
    }

    public async Task<Medicine> AddAsync(Medicine medicine)
    {
        var medicines = (await GetAllAsync()).ToList();

        medicine.Id = medicines.Count == 0
            ? 1
            : medicines.Max(m => m.Id) + 1;

        medicines.Add(medicine);

        var json = JsonSerializer.Serialize(
            medicines,
            _jsonOptions);

        await File.WriteAllTextAsync(_filePath, json);

        return medicine;
    }

    public async Task UpdateAsync(Medicine medicine)
    {
        var medicines = (await GetAllAsync()).ToList();

        var existingMedicine = medicines.FirstOrDefault(m => m.Id == medicine.Id);

        if (existingMedicine is null)
        {
            throw new InvalidOperationException("Medicine not found.");
        }

        existingMedicine.FullName = medicine.FullName;
        existingMedicine.Notes = medicine.Notes;
        existingMedicine.ExpiryDate = medicine.ExpiryDate;
        existingMedicine.Quantity = medicine.Quantity;
        existingMedicine.Price = medicine.Price;
        existingMedicine.Brand = medicine.Brand;

        var json = JsonSerializer.Serialize(
            medicines,
            _jsonOptions);

        await File.WriteAllTextAsync(_filePath, json);
        }
}