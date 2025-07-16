using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.RecorderSettings.Queries.GetById;

public class GetRecorderPointQuery : IRequest<Result<GetRecorderPointResponse>>
{
    public Guid Id { get; set; }
}
