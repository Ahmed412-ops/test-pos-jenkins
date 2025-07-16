using Pharmacy.Application.Dto.Common.Queries;

namespace Pharmacy.Application.Features.Expense.Category.Queries.GetById;

public class GetExpenseCategoryResponse : CommonQueryResponseBase
{
    public bool IsActive { get; set; }
}
