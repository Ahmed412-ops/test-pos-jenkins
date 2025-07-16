using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Shifts.CashExpenses.Queries.GetById;

public class GetCashExpenseQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) :
    BaseHandler<GetCashExpenseQuery, Result<GetCashExpenseResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Expense.CashExpense> _cashExpenseRepository =
        unitOfWork.GetRepository<Domain.Entities.Wallets.Expense.CashExpense>();
    public override async Task<Result<GetCashExpenseResponse>> Handle(GetCashExpenseQuery request, CancellationToken cancellationToken)
    {
        var query = await _cashExpenseRepository.FindAsync(
            a => a.Id == request.Id && !a.Is_Deleted,
            Include: a => a.Include(a => a.Category)
            .Include(a=>a.ShiftWallet)
            .ThenInclude(a=>a.Wallet)
                
        );

        if (query is null)
            return Result<GetCashExpenseResponse>.Fail(Messages.NotFound);

        var response = mapper.Map<GetCashExpenseResponse>(query);
        return Result<GetCashExpenseResponse>.Success(response);
    }
}
