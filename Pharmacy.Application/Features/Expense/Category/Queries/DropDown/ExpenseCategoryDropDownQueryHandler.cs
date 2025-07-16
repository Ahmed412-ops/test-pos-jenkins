using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Expense.Category.Queries.DropDown;

public class ExpenseCategoryDropDownQueryHandler(IUnitOfWork unitOfWork , IMapper mapper)
    : BaseHandler<ExpenseCategoryDropDownQuery, Result<List<DropDownQueryResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Expense.ExpenseCategory>
    _expenseCategoryRepository = unitOfWork.GetRepository<Domain.Entities.Wallets.Expense.ExpenseCategory>();


    public override async Task<Result<List<DropDownQueryResponse>>> Handle(ExpenseCategoryDropDownQuery request, CancellationToken cancellationToken)
    {
        var customer = await _expenseCategoryRepository.GetAllAsync(d => !d.Is_Deleted);
        var result = mapper.Map<List<DropDownQueryResponse>>(customer);
        return Result<List<DropDownQueryResponse>>.Success(result);
    }
}
