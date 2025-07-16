using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Shifts.CashExpenses.Commands.Delete;

public class DeleteCashExpenseCommandHandler(IUnitOfWork unitOfWork)
    : BaseHandler<DeleteCashExpenseCommand, Result<bool>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Expense.CashExpense>
    _cashExpenseRepository = unitOfWork.GetRepository<Domain.Entities.Wallets.Expense.CashExpense>();

    public override async Task<Result<bool>> Handle(DeleteCashExpenseCommand request, CancellationToken cancellationToken)
    {
        var expenseCategory = await _cashExpenseRepository.FindAsync(c => c.Id == request.Id);
        if (expenseCategory == null)
            return Result<bool>.Fail(Messages.NotFound);
        expenseCategory.Is_Deleted = true;
        int result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<bool>.Fail(Messages.SomethingWentWrong);
        return Result<bool>.Success(true, Messages.DeletedSuccessfully);

    }
}
