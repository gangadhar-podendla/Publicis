namespace Pharmacy.Api.Models;

public class Sale
{
    public int Id { get; set; }

    public int MedicineId { get; set; }

    public string MedicineName { get; set; } = string.Empty;

    public int QuantitySold { get; set; }

    public DateTime SoldAt { get; set; }
}