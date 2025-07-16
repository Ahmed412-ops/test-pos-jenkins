using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.DosageForm.Queries.GetById;

public class GetDosageFormQuery : IRequest<Result<GetDosageFormResponse>>
{
    public Guid Id { get; set; }
}
