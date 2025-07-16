using MediatR;
using Pharmacy.Application.Features.Stock.Medication.Commands.Create;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.Medication.Commands.Update;

public class UpdateMedicationStockCommand : CreateMedicationStockCommand, IRequest<Result<string>>
{
    public Guid Id { get; set; }
}

