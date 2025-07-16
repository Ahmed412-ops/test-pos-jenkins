using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Expense.Category.Commands.Update;

public class UpdateExpenseCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<UpdateExpenseCategoryCommand, Result<string>>

{
    private readonly IGenericRepository<Domain.Entities.Wallets.Expense.ExpenseCategory>
        _expenseCategoryRepository = unitOfWork.GetRepository<Domain.Entities.Wallets.Expense.ExpenseCategory>();

    public override async Task<Result<string>> Handle(UpdateExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _expenseCategoryRepository.FindAsync(a => a.Id == request.Id);
        if (entity is null)
            return Result<string>.Fail(Messages.ExpenseCategoryNotFound);

        mapper.Map(request, entity); 

        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.SuccessfullyUpdated);
    }
}
