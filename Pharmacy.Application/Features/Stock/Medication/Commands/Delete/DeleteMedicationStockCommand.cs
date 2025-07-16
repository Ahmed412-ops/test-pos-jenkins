using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.Medication.Commands.Delete;

public class DeleteMedicationStockCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}
