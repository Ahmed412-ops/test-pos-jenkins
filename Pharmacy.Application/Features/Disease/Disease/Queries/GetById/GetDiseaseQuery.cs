using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Disease.Disease.Queries.GetById;

public class GetDiseaseQuery : IRequest<Result<GetDiseaseResponse>>
{
    public Guid Id { get; set; }
}
