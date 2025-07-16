using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SaleScreen.Prescription.Queries.GetById;

public class GetPrescriptionQuery : IRequest<Result<GetPrescriptionResponse>>
{
    public Guid Id { get; set; }
}
