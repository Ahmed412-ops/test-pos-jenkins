using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Expense.Category.Queries.GetAll;

public class GetExpensesCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetExpensesCategoriesQuery, Result<PaginationResponse<GetExpensesCategoriesResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Expense.ExpenseCategory>
    _expenseCategoryRepository = unitOfWork.GetRepository<Domain.Entities.Wallets.Expense.ExpenseCategory>();

    public override async Task<Result<PaginationResponse<GetExpensesCategoriesResponse>>> Handle(GetExpensesCategoriesQuery request, CancellationToken cancellationToken)
    {
        var query = await _expenseCategoryRepository.GetAllQueryableAsync(c => !c.Is_Deleted
      );

        if (!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(c => c.Name.Contains(request.Name));

        var count = await query.CountAsync(cancellationToken);

        var response = await query
            .Select(c => mapper.Map<GetExpensesCategoriesResponse>(c))
            .Paginate(request)
            .ToListAsync(cancellationToken);

        return Result<PaginationResponse<GetExpensesCategoriesResponse>>.Success(
        new PaginationResponse<GetExpensesCategoriesResponse> { Data = response, Count = count }
    );

    }
}
