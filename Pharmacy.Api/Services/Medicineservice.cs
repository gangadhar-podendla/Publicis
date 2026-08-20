using Pharmacy.Api.Models;
using Pharmacy.Api.Repositories.Interfaces;
using Pharmacy.Api.Services.Interfaces;

namespace Pharmacy.Api.Services;

public class MedicineService : IMedicineService
{
    private readonly IMedicineRepository _repository;

    public MedicineService(IMedicineRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Medicine>> GetAllAsync(string? search = null)
    {
        var medicines = await _repository.GetAllAsync();

        if (string.IsNullOrWhiteSpace(search))
        {
            return medicines;
        }

        return medicines.Where(m =>
            m.FullName.Contains(
                search,
                StringComparison.OrdinalIgnoreCase));
    }

    public async Task<Medicine> AddAsync(Medicine medicine)
    {
        return await _repository.AddAsync(medicine);
    }
}