using MediatR;
using Pharmacy.Application.Features.SaleScreen.Prescription.Queries.GetById;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SaleScreen.Prescription.Commands.Create;

public class CreatePrescriptionCommand : IRequest<Result<GetPrescriptionResponse>>
{
    public Guid? CustomerId { get; set; }
    public Guid ShiftId { get; set; }
    public decimal CashbackUsed { get; set; } = 0m;
    public decimal CreditUsed { get; set; } = 0m;
    public bool IsTransferred { get; set; } = false;
    public string? Notes { get; set; }
    public List<CreatePrescriptionItemDto> PrescriptionItems { get; set; } = [];
    public List<PaymentTransactionDto> Payments { get; set; } = [];
}

public class CreatePrescriptionItemDto
{
    public required Guid MedicationStockId { get; set; }
    public required Guid MedicineUnitId { get; set; }
    public int Quantity { get; set; }
    public decimal AppliedDiscount { get; set; } // Discount applied to the unit price
}

public class PaymentTransactionDto
{
    public Guid ShiftWalletId { get; set; }
    public decimal Amount { get; set; }
}
