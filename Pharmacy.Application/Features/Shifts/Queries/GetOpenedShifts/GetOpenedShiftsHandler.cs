using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Shifts.Queries.GetOpenedShifts;

public class GetOpenedShiftsHandler(
IUnitOfWork unitOfWork, IMapper mapper
)
: BaseHandler<GetOpenedShiftsQuery, Result<List<GetOpenedShiftsResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Shift> _shiftRepository =
        unitOfWork.GetRepository<Domain.Entities.Wallets.Shift>();
    public override async Task<Result<List<GetOpenedShiftsResponse>>> Handle(
        GetOpenedShiftsQuery request, CancellationToken cancellationToken)
    {
        var openShifts = await _shiftRepository
            .GetAllQueryableAsync(s =>
                !s.Is_Deleted                 
            && s.ClosedAt == null
            , Include : s => s
                .Include(x => x.OpenedBy));

        var result = mapper.Map<List<GetOpenedShiftsResponse>>(openShifts);

        return Result<List<GetOpenedShiftsResponse>>.Success(result);
    }
}