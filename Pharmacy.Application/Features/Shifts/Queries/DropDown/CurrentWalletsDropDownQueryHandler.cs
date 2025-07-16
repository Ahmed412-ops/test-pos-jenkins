using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Shifts.Queries.DropDown
{
    public class CurrentWalletsDropDownQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICurrentUser currentUser
    ) : BaseHandler<CurrentWalletsDropDownQuery, Result<List<CurrentWalletsDropDownResponse>>>
    {
        private readonly IGenericRepository<Domain.Entities.Wallets.ShiftWallet> _shiftWalletRepository =
            unitOfWork.GetRepository<Domain.Entities.Wallets.ShiftWallet>();

        public override async Task<Result<List<CurrentWalletsDropDownResponse>>> Handle(
            CurrentWalletsDropDownQuery request,
            CancellationToken cancellationToken
        )
        {
            var currentUserId = currentUser.GetUserId();

            var wallet = await _shiftWalletRepository.GetAllAsync(
                c =>
                    !c.Is_Deleted
                    && c.Shift.ClosedAt == null
                    && c.Shift.OpenedById == currentUserId,
                Include: q => q.Include(a => a.Shift).Include(a => a.Wallet)
            );
            var result = mapper.Map<List<CurrentWalletsDropDownResponse>>(wallet);
            return Result<List<CurrentWalletsDropDownResponse>>.Success(result);
        }
    }
}
