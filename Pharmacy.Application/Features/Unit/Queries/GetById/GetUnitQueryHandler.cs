using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Unit.Queries.GetById;

public class GetUnitQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetUnitQuery, Result<GetUnitResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Unit.Unit> _unitRepo = unitOfWork.GetRepository<Domain.Entities.Unit.Unit>();

    public override async Task<Result<GetUnitResponse>> Handle(
        GetUnitQuery request,
        CancellationToken cancellationToken
    )
    {
        var unit = await _unitRepo.FindAsync(m => m.Id == request.Id);

        if (unit == null)
            return Result<GetUnitResponse>.Fail(Messages.UnitNotFound);

        var response = mapper.Map<GetUnitResponse>(unit);
        return Result<GetUnitResponse>.Success(response);
    }
}

