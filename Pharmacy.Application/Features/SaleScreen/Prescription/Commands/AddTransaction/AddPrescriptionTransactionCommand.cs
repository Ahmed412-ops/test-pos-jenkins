using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SaleScreen.Prescription.Commands.AddTransaction;

public class AddPrescriptionTransactionCommand : IRequest<Result<AddPrescriptionTransactionResponse>>
{
    public Guid PrescriptionId { get; set; }
    public Guid ShiftWalletId { get; set; }
    public decimal AmountPaid { get; set; }
}
