namespace Pharmacy.Api.Models;

public class Medicine
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public DateTime ExpiryDate { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public string Brand { get; set; } = string.Empty;
}