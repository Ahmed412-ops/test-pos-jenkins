using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Wallets.Expense;

namespace Pharmacy.Application.Features.Shifts.CashExpenses.Commands.Create;

public class CreateCashExpenseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<CreateCashExpenseCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Expense.CashExpense>
    _cashExpenseRepository = unitOfWork.GetRepository<Domain.Entities.Wallets.Expense.CashExpense>();
    private readonly IGenericRepository<Domain.Entities.Wallets.ShiftWallet>
    _shiftWalletRepo = unitOfWork.GetRepository<Domain.Entities.Wallets.ShiftWallet>();


    public override async Task<Result<string>> Handle(CreateCashExpenseCommand request, CancellationToken cancellationToken)
    {
        var shiftwallet = await _shiftWalletRepo.FindAsync(
            x => x.WalletId == request.WalletId && x.Shift.ClosedAt == null,
            Include: q => q.Include(x => x.Shift)
            );
        var entity = mapper.Map<CashExpense>(request);
        entity.ShiftWalletId = shiftwallet!.Id;
        entity.ExpenseDateTime = DateTime.UtcNow;
        await _cashExpenseRepository.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.CreateCashExpenseSuccess);

    }
}
