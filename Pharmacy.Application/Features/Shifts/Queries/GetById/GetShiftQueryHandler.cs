using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Shifts.Queries.GetById;

public class GetShiftQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetShiftQuery, Result<GetShiftResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Shift> _shiftRepo =
        unitOfWork.GetRepository<Domain.Entities.Wallets.Shift>();

    public override async Task<Result<GetShiftResponse>> Handle(
        GetShiftQuery request,
        CancellationToken cancellationToken)
    {
        var shift = await _shiftRepo.FindAsync(
            s => s.Id == request.Id,
            Include: s => s.Include(i => i.ShiftWallets)
                    .ThenInclude(i => i.Wallet)
                    .Include(i => i.ShiftWallets)
                    .ThenInclude(i => i.CashExpenses)
                    .Include(i => i.ShiftWallets)
                    .ThenInclude(i => i.PrescriptionTransactions),
            asNoTracking: true);

        if (shift == null)
            return Result<GetShiftResponse>.Fail(Messages.ShiftNotFound);

        var response = mapper.Map<GetShiftResponse>(shift);

        return Result<GetShiftResponse>.Success(response);
    }
}
