using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Shifts.Queries.GetAll;

public class GetShiftsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetShiftsQuery, Result<PaginationResponse<GetShiftsResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Shift> _shiftsRepo =
        unitOfWork.GetRepository<Domain.Entities.Wallets.Shift>();

    public override async Task<Result<PaginationResponse<GetShiftsResponse>>> Handle(
        GetShiftsQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _shiftsRepo.GetAllQueryableAsync(
            x => !x.Is_Deleted,
            Include: x =>
                x.Include(i => i.ShiftWallets).ThenInclude(m => m.Wallet).Include(i => i.OpenedBy)
        );

        if (request.OpenedBy != null)
            query = query.Where(x => x.OpenedBy.Full_Name.Contains(request.OpenedBy));

        if (request.FromDate != null)
            query = query.Where(x => x.OpenedAt >= request.FromDate);

        if (request.ToDate != null)
            query = query.Where(x => x.OpenedAt <= request.ToDate);

        if (request.IsOpen != null)
        {
            if (request.IsOpen.Value)
                query = query.Where(x => x.ClosedAt == null);
            else
                query = query.Where(x => x.ClosedAt != null);
        }

        var count = await query.CountAsync(cancellationToken);

        var response = await query
            .Select(x => mapper.Map<GetShiftsResponse>(x))
            .Paginate(request)
            .ToListAsync(cancellationToken);

        return Result<PaginationResponse<GetShiftsResponse>>.Success(
            new PaginationResponse<GetShiftsResponse> { Data = response, Count = count }
        );
    }
}
