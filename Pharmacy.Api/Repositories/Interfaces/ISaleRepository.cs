using Pharmacy.Api.Models;

namespace Pharmacy.Api.Repositories.Interfaces;

public interface ISaleRepository
{
    Task<IEnumerable<Sale>> GetAllAsync();

    Task<Sale> AddAsync(Sale sale);
}