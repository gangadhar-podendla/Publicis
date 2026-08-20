using Microsoft.AspNetCore.Mvc;
using Pharmacy.Api.Models;
using Pharmacy.Api.Services.Interfaces;

namespace Pharmacy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicinesController : ControllerBase
{
    private readonly IMedicineService _medicineService;

    public MedicinesController(IMedicineService medicineService)
    {
        _medicineService = medicineService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Medicine>>> GetAll(
        [FromQuery] string? search)
    {
        var medicines = await _medicineService.GetAllAsync(search);

        return Ok(medicines);
    }

    [HttpPost]
    public async Task<ActionResult<Medicine>> Add(Medicine medicine)
    {
        var createdMedicine = await _medicineService.AddAsync(medicine);

        return Ok(createdMedicine);
    }
}