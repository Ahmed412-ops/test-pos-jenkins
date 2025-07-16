using Pharmacy.Application.Dto.Common.Queries;

namespace Pharmacy.Application.Features.Expense.Category.Queries.GetAll;

public class GetExpensesCategoriesResponse : CommonQueryResponseBase
{
    public bool IsActive { get; set; }
}
