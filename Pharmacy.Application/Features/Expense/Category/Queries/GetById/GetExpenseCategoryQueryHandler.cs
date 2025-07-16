
using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Expense.Category.Queries.GetById;

public class GetExpenseCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetExpenseCategoryQuery, Result<GetExpenseCategoryResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Expense.ExpenseCategory>
_expenseCategoryRepository = unitOfWork.GetRepository<Domain.Entities.Wallets.Expense.ExpenseCategory>();


    public override async Task<Result<GetExpenseCategoryResponse>> Handle(GetExpenseCategoryQuery request, CancellationToken cancellationToken)
    {
        var expenseCategory = await _expenseCategoryRepository.FindAsync
            (a => a.Id == request.Id);
        if (expenseCategory is null)
            Result<GetExpenseCategoryResponse>.Fail(Messages.ExpenseCategoryNotFound);

        var response = mapper.Map<GetExpenseCategoryResponse>(expenseCategory);

        return Result<GetExpenseCategoryResponse>.Success(response);


    }
}
