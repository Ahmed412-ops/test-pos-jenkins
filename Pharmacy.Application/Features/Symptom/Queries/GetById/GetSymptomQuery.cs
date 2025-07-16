using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Symptom.Queries.GetById;

public class GetSymptomQuery : IRequest<Result<GetSymptomResponse>>
{
    public Guid Id { get; set; }    
}
