using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.Return.Queries.GetById;

public class GetMedicationReturnQuery : IRequest<Result<GetMedicationReturnResponse>>
{
    public Guid Id { get; set; }
}
