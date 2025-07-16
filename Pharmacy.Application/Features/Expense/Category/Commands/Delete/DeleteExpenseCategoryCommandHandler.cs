using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Expense.Category.Commands.Delete;

public class DeleteExpenseCategoryCommandHandler(IUnitOfWork unitOfWork)
    : BaseHandler<DeleteExpenseCategoryCommand, Result<bool>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Expense.ExpenseCategory> _expenseCategoryRepository
        = unitOfWork.GetRepository<Domain.Entities.Wallets.Expense.ExpenseCategory>();
    public override async Task<Result<bool>> Handle(DeleteExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        var expenseCategory = await _expenseCategoryRepository.FindAsync(c => c.Id == request.Id);
        if (expenseCategory == null)
            return Result<bool>.Fail(Messages.NotFound);
        expenseCategory.Is_Deleted = true;
        expenseCategory.IsActive = false;
        int result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<bool>.Fail(Messages.SomethingWentWrong);
        return Result<bool>.Success(true, Messages.DeletedSuccessfully);
    }
}
