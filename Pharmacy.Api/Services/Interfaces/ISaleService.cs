using Pharmacy.Api.Models;

namespace Pharmacy.Api.Services.Interfaces;

public interface ISaleService
{
    Task<IEnumerable<Sale>> GetAllAsync();

    Task<Sale> SellAsync(int medicineId, int quantity);
}