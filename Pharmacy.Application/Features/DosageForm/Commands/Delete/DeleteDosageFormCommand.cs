using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.DosageForm.Commands.Delete;

public class DeleteDosageFormCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}
