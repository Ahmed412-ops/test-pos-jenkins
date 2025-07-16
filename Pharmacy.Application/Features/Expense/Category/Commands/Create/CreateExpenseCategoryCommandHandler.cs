using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Wallets.Expense;

namespace Pharmacy.Application.Features.Expense.Category.Commands.Create
{
    public class CreateExpenseCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<CreateExpenseCategoryCommand, Result<string>>
    {
        private readonly IGenericRepository<Domain.Entities.Wallets.Expense.ExpenseCategory>
            _expenseCategoryRepository = unitOfWork.GetRepository<Domain.Entities.Wallets.Expense.ExpenseCategory>();
        public override async Task<Result<string>> Handle(CreateExpenseCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<ExpenseCategory>(request);

            await _expenseCategoryRepository.AddAsync(entity);
            await unitOfWork.SaveChangesAsync();

            return Result<string>.Success(Messages.SuccessfullyCreated);
        }
    }
}
