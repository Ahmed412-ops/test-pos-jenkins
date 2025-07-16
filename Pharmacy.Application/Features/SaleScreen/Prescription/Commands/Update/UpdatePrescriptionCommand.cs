using MediatR;
using Pharmacy.Application.Features.SaleScreen.Prescription.Commands.Create;
using Pharmacy.Application.Features.SaleScreen.Prescription.Queries.GetById;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SaleScreen.Prescription.Commands.Update;

public class UpdatePrescriptionCommand : IRequest<Result<GetPrescriptionResponse>>
{
    public Guid PrescriptionId { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid ShiftId { get; set; }
    public decimal CashbackUsed { get; set; } = 0m;
    public decimal CreditUsed { get; set; } = 0m;
    public string? Notes { get; set; }
    public List<UpdatePrescriptionItemDto> PrescriptionItems { get; set; } = [];
    public List<PaymentTransactionDto> Payments { get; set; } = [];

}
public class UpdatePrescriptionItemDto : CreatePrescriptionItemDto
{
    public Guid PrescriptionItemId { get; set; }
}