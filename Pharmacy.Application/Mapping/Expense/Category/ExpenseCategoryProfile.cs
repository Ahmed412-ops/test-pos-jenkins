using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Expense.Category.Commands.Create;
using Pharmacy.Application.Features.Expense.Category.Commands.Update;
using Pharmacy.Application.Features.Expense.Category.Queries.GetAll;
using Pharmacy.Application.Features.Expense.Category.Queries.GetById;
using Pharmacy.Domain.Entities.Wallets.Expense;

namespace Pharmacy.Application.Mapping.Expense.Category;

public class ExpenseCategoryProfile : MappingProfileBase
{
    public ExpenseCategoryProfile()
    {
        CreateMap<CreateExpenseCategoryCommand, ExpenseCategory>();
        CreateMap<UpdateExpenseCategoryCommand, ExpenseCategory>()
             .IncludeBase<CreateExpenseCategoryCommand, Domain.Entities.Wallets.Expense.ExpenseCategory>();

        CreateMap<ExpenseCategory, GetExpensesCategoriesResponse>();
        CreateMap<Domain.Entities.Wallets.Expense.ExpenseCategory, DropDownQueryResponse>();
        CreateMap<Domain.Entities.Wallets.Expense.ExpenseCategory, GetExpenseCategoryResponse>();
    }
}
