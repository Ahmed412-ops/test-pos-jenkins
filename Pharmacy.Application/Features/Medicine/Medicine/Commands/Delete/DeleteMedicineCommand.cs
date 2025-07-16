using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Medicine.Medicine.Commands.Delete;

public class DeleteMedicineCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}
