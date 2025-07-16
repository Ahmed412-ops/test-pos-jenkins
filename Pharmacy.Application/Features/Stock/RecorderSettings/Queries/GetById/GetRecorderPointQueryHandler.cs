using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.RecorderSettings.Queries.GetById;

public class GetRecorderPointQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<GetRecorderPointQuery, Result<GetRecorderPointResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Stock.RecorderPointSettings> _recorderSettingsRepo = unitOfWork.GetRepository<Domain.Entities.Stock.RecorderPointSettings>();
    public override async Task<Result<GetRecorderPointResponse>> Handle(GetRecorderPointQuery request, CancellationToken cancellationToken)
    {
        var recorderSettings = await _recorderSettingsRepo.FindAsync(
            s => s.Id == request.Id,
            Include: s => s.Include(i => i.Medicine)
                            .Include(i => i.PreferredSupplier!),
            asNoTracking: true);

        if (recorderSettings == null)
            return Result<GetRecorderPointResponse>.Fail(Messages.NotFound);

        var response = mapper.Map<GetRecorderPointResponse>(recorderSettings);

        return Result<GetRecorderPointResponse>.Success(response);
    }
}

