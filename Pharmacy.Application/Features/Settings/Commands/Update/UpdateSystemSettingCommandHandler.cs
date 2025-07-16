using AutoMapper;
using MediatR;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Settings;

namespace Pharmacy.Application.Features.Settings.Commands.Update;

public class UpdateSystemSettingCommandHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateSystemSettingCommand, Result<string>>
{
    private readonly IGenericRepository<SystemSetting> _repo = unitOfWork.GetRepository<SystemSetting>();

    public async Task<Result<string>> Handle(UpdateSystemSettingCommand request, CancellationToken cancellationToken)
    {
        var setting = await _repo.FindAsync(a=>a.Id == request.Id);
        if (setting is null)
            return Result<string>.Fail(Messages.NotFound);
        setting.Value = request.Value;
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Success(Messages.SuccessfullyUpdated);
    }
}
