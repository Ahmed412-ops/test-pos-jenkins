using MediatR;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Settings;

namespace Pharmacy.Application.Features.Settings.Commands.Delete;

public class DeleteSystemSettingCommandHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<DeleteSystemSettingCommand, Result<bool>>
{
    private readonly IGenericRepository<SystemSetting> _repo = unitOfWork.GetRepository<SystemSetting>();

    public async Task<Result<bool>> Handle(DeleteSystemSettingCommand request, CancellationToken cancellationToken)
    {
        var setting = await _repo.FindAsync(x=>x.Id== request.Id);
        if (setting is null)
            return Result<bool>.Fail(Messages.NotFound);
        setting.Is_Deleted = true;
        int result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<bool>.Fail(Messages.SomethingWentWrong);
        return Result<bool>.Success(true, Messages.DeletedSuccessfully);
    }
}
