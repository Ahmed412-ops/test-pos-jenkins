using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Unit.Commands.Update;

public class UpdateUnitCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<UpdateUnitCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Unit.Unit> _unitRepository = unitOfWork.GetRepository<Domain.Entities.Unit.Unit>();

    public override async Task<Result<string>> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
    {
        var unit = await _unitRepository.FindAsync(
            s => s.Id == request.Id);

        if (unit == null)
            return Result<string>.Fail(Messages.UnitNotFound);

        mapper.Map(request, unit);

        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.SuccessfullyUpdated);
    }
}
