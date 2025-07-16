using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.RecorderSettings.Commands.Create;

public class CreateRecorderPointCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<CreateRecorderPointCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Stock.RecorderPointSettings> _recorderSettingsRepository =
        unitOfWork.GetRepository<Domain.Entities.Stock.RecorderPointSettings>();

    public override async Task<Result<string>> Handle(
        CreateRecorderPointCommand request,
        CancellationToken cancellationToken
    )
    {
        var recorderSettings = mapper.Map<Domain.Entities.Stock.RecorderPointSettings>(request);
       
        await _recorderSettingsRepository.AddAsync(recorderSettings);
        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(message: Messages.SuccessfullyCreated);
    }
}
