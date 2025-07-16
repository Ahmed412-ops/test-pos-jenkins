namespace Pharmacy.Application.Features.Stock.Medication.Queries.GetNearExpiryMedicines;

public class GetNearExpiryMedicinesResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int QuantityInStock { get; set; }
    public string ExpiryDate { get; set; } = string.Empty;
    public int DaysUntilExpiry { get; set; }
    public decimal SellingPrice { get; set; }
}
