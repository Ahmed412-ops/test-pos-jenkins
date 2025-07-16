using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Unit.Commands.Create;

public class CreateUnitCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<CreateUnitCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Unit.Unit> _unitRepository = unitOfWork.GetRepository<Domain.Entities.Unit.Unit>();

    public override async Task<Result<string>> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
    {
        var unit = mapper.Map<Domain.Entities.Unit.Unit>(request);
        await _unitRepository.AddAsync(unit);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Success(Messages.SuccessfullyCreated);
    }
}

