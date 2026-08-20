using Pharmacy.Api.Models;

namespace Pharmacy.Api.Services.Interfaces;

public interface IMedicineService
{
    Task<IEnumerable<Medicine>> GetAllAsync(string? search = null);

    Task<Medicine> AddAsync(Medicine medicine);
}