using MediatR;
using Pharmacy.Application.Features.Stock.Return.Commands.Create;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.Return.Commands.Update;

public class UpdateReturnStockCommand : CreateMedicationReturnCommand, IRequest<Result<string>>
{
    public Guid Id { get; set; }
}
