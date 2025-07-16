using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Expense.Category.Commands.ChangeActivation;

public class ChangeExpenseCategoryActivationCommandHandler(IUnitOfWork unitOfWork)
    : BaseHandler<ChangeExpenseCategoryActivationCommand, Result<bool>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Expense.ExpenseCategory>
    _expenseCategoryRepository = unitOfWork.GetRepository<Domain.Entities.Wallets.Expense.ExpenseCategory>();

    public override async Task<Result<bool>> Handle(ChangeExpenseCategoryActivationCommand request, CancellationToken cancellationToken)
    {
        var category = await _expenseCategoryRepository.FindAsync(a => a.Id == request.CategoryId);
        if (category == null)
            return Result<bool>.Fail(Messages.NotFound);

        category.IsActive = !category.IsActive;
        await _expenseCategoryRepository.SaveChangesAsync();
        return Result<bool>.Success(Messages.ChangeExpenseCategoryActivation);

    }
}
