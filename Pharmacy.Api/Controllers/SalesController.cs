using Microsoft.AspNetCore.Mvc;
using Pharmacy.Api.Models;
using Pharmacy.Api.Services.Interfaces;

namespace Pharmacy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly ISaleService _saleService;

    public SalesController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Sale>>> GetAll()
    {
        var sales = await _saleService.GetAllAsync();

        return Ok(sales);
    }

    [HttpPost]
    public async Task<ActionResult<Sale>> Sell(SaleRequest request)
    {
        try
        {
            var sale = await _saleService.SellAsync(
                request.MedicineId,
                request.Quantity);

            return Ok(sale);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}