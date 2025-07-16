using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Shifts.CashExpenses.Queries.GetAll
{
    public class GetCashExpensesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : BaseHandler<GetCashExpensesQuery, Result<PaginationResponse<GetCashExpensesResponse>>>
    {
        private readonly IGenericRepository<Domain.Entities.Wallets.Expense.CashExpense>
        _cashExpenseRepository = unitOfWork.GetRepository<Domain.Entities.Wallets.Expense.CashExpense>();

        public override async Task<Result<PaginationResponse<GetCashExpensesResponse>>> Handle(GetCashExpensesQuery request, CancellationToken cancellationToken)
        {
            var query = await _cashExpenseRepository.GetAllQueryableAsync(c => !c.Is_Deleted
            ,Include: q=>q.Include(c=>c.Category)
            .Include(a=>a.ShiftWallet)
            .ThenInclude(a=>a.Shift)
            .ThenInclude(a=>a.OpenedBy));

            #region Filters

            if (!string.IsNullOrWhiteSpace(request.CategoryName))
                query = query.Where(c => c.Category.Name.Contains(request.CategoryName));

            if (request.DateFrom != default)
                query = query.Where(c => c.ExpenseDateTime.Date >= request.DateFrom.ToDateTime(TimeOnly.MinValue));

            if (request.DateTo != default)
                query = query.Where(c => c.ExpenseDateTime.Date <= request.DateTo.ToDateTime(TimeOnly.MaxValue));

            if (!string.IsNullOrWhiteSpace(request.Full_Name))
                query = query.Where(c => c.ShiftWallet.Shift.OpenedBy.Full_Name!.Contains(request.Full_Name));

            if (request.IsOpen != null)
            {
                if (request.IsOpen.Value)
                    query = query.Where(x => x.ShiftWallet.Shift.ClosedAt == null); 
                else
                    query = query.Where(x => x.ShiftWallet.Shift.ClosedAt != null);
            }
            #endregion

            var count = await query.CountAsync(cancellationToken);

            var response = await query
                .Select(c => mapper.Map<GetCashExpensesResponse>(c))
                .Paginate(request)
                .ToListAsync(cancellationToken);

            return Result<PaginationResponse<GetCashExpensesResponse>>.Success(
                new PaginationResponse<GetCashExpensesResponse> { Data = response, Count = count }
                );
        }
    }
}
