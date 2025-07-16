using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Shifts.Commands.Create;

public class OpenShiftCommandHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICurrentUser currentUser
) : BaseHandler<OpenShiftCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Shift> _shiftRepository =
        unitOfWork.GetRepository<Domain.Entities.Wallets.Shift>();

    public override async Task<Result<string>> Handle(
        OpenShiftCommand request,
        CancellationToken cancellationToken
    )
    {
        var shift = mapper.Map<Domain.Entities.Wallets.Shift>(request);
        shift.OpenedById = currentUser.GetUserId();
        await _shiftRepository.AddAsync(shift);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Success(Messages.SuccessfullyCreated);
    }
}
