using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Shifts.CashExpenses.Commands.Update;

public class UpdateCashExpenseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUser currentUser)
    : BaseHandler<UpdateCashExpenseCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Expense.CashExpense> _cashExpenseRepository =
    unitOfWork.GetRepository<Domain.Entities.Wallets.Expense.CashExpense>();
    public override async Task<Result<string>> Handle(UpdateCashExpenseCommand request, CancellationToken cancellationToken)
    {
        var entity = await _cashExpenseRepository.FindAsync(x => x.Id == request.Id,
            Include: q => q.Include(w => w.ShiftWallet)
            .ThenInclude(x => x.Shift).ThenInclude(x => x.OpenedBy)
            
            );
        if (entity is null)
            return Result<string>.Fail(Messages.NotFound);

        var shift = entity.ShiftWallet.Shift;

        if (shift.ClosedAt is not null &&
            currentUser.GetUserRole() != UserRole.Admin &&
            currentUser.GetUserRole() != UserRole.SuperAdmin)
            return Result<string>.Fail(Messages.ShiftAlreadyClosed);
        
        mapper.Map(request, entity);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Success(Messages.SuccessfullyUpdated);
    }
}
