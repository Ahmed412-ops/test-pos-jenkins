using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Medicine.Medicine.Queries.GetById;

public class GetMedicineQuery : IRequest<Result<GetMedicineResponse>>
{
    public Guid Id { get; set; }
}
