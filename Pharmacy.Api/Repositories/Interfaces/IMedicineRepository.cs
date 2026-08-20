using Pharmacy.Api.Models;

namespace Pharmacy.Api.Repositories.Interfaces;

public interface IMedicineRepository
{
    Task<IEnumerable<Medicine>> GetAllAsync();

    Task<Medicine> AddAsync(Medicine medicine);

    Task UpdateAsync(Medicine medicine);
}