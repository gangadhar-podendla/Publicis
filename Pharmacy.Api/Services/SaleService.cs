using Pharmacy.Api.Models;
using Pharmacy.Api.Repositories.Interfaces;
using Pharmacy.Api.Services.Interfaces;

namespace Pharmacy.Api.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMedicineRepository _medicineRepository;

    public SaleService(
        ISaleRepository saleRepository,
        IMedicineRepository medicineRepository)
    {
        _saleRepository = saleRepository;
        _medicineRepository = medicineRepository;
    }

    public async Task<IEnumerable<Sale>> GetAllAsync()
    {
        return await _saleRepository.GetAllAsync();
    }

    public async Task<Sale> SellAsync(int medicineId, int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.");
        }

        var medicines = await _medicineRepository.GetAllAsync();

        var medicine = medicines.FirstOrDefault(m => m.Id == medicineId);

        if (medicine is null)
        {
            throw new InvalidOperationException("Medicine not found.");
        }

        if (medicine.Quantity < quantity)
        {
            throw new InvalidOperationException("Insufficient stock.");
        }

        medicine.Quantity -= quantity;

        await _medicineRepository.UpdateAsync(medicine);

        var sale = new Sale
        {
            MedicineId = medicine.Id,
            MedicineName = medicine.FullName,
            QuantitySold = quantity,
            SoldAt = DateTime.UtcNow
        };

        return await _saleRepository.AddAsync(sale);
    }
}