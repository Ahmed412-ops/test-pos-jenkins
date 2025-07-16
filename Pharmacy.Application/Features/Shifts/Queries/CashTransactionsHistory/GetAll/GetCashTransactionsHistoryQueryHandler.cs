// using Microsoft.EntityFrameworkCore;
// using Pharmacy.Application.Common.Interfaces;
// using Pharmacy.Application.Dto;
// using Pharmacy.Application.Helper.Extensions;
// using Pharmacy.Domain.Dto;

// namespace Pharmacy.Application.Features.Shifts.Queries.CashTransactionsHistory.GetAll
// {
//     public class GetCashTransactionsHistoryQueryHandler(IUnitOfWork unitOfWork)
//         : BaseHandler<
//             GetCashTransactionsHistoryQuery,
//             Result<PaginationResponse<GetCashTransactionsHistoryResponse>>
//         >
//     {
//         private readonly IGenericRepository<Domain.Entities.Wallets.Sales.Prescription> _prescriptionRepo =
//             unitOfWork.GetRepository<Domain.Entities.Wallets.Sales.Prescription>();

//         private readonly IGenericRepository<Domain.Entities.Wallets.Expense.CashExpense> _cashExpenseRepo =
//             unitOfWork.GetRepository<Domain.Entities.Wallets.Expense.CashExpense>();

//         public override async Task<
//             Result<PaginationResponse<GetCashTransactionsHistoryResponse>>
//         > Handle(GetCashTransactionsHistoryQuery request, CancellationToken cancellationToken)
//         {
//             var salesQuery = await _prescriptionRepo.GetAllQueryableAsync(
//                 x => !x.Is_Deleted,
//                 q =>
//                     q.Include(p => p.ShiftWallet)
//                         .ThenInclude(sw => sw.Wallet)
//                         .Include(p => p.ShiftWallet)
//                         .ThenInclude(sw => sw.Shift)
//                         .ThenInclude(s => s.OpenedBy)
//             );

//             var expenseQuery = await _cashExpenseRepo.GetAllQueryableAsync(
//                 x => !x.Is_Deleted,
//                 q =>
//                     q.Include(e => e.ShiftWallet)
//                         .ThenInclude(sw => sw.Wallet)
//                         .Include(e => e.ShiftWallet)
//                         .ThenInclude(sw => sw.Shift)
//                         .ThenInclude(s => s.OpenedBy)
//                         .Include(e => e.Category)
//             );

//             var salesProjection = salesQuery.Select(a => new
//             {
//                 a.Id,
//                 Type = "Incoming",
//                 WalletName = a.ShiftWallet.Wallet.Name,
//                 a.Notes,
//                 a.Amount,
//                 PerformedBy = a.ShiftWallet.Shift.OpenedBy.Full_Name,
//                 CustomerName = a.Customer != null ? a.Customer.Name : null,
//                 ExpenseCategory = (string?)null, // Ensure nullability matches expense projection
//                 Date = a.Created_At,
//             });
//             var expenseProjection = expenseQuery.Select(e => new
//             {
//                 e.Id,
//                 Type = "Outgoing",
//                 WalletName = e.ShiftWallet.Wallet.Name,
//                 e.Notes,
//                 e.Amount,
//                 PerformedBy = e.ShiftWallet.Shift.OpenedBy.Full_Name,
//                 CustomerName = (string?)null, // Add this to match sales projection
//                 ExpenseCategory = (string?)e.Category.Name, // Ensure nullability matches sales projection
//                 Date = e.ExpenseDateTime,
//             });

//             var combinedQuery = salesProjection.Concat(expenseProjection);

//             if (request.TransactionType != null)
//             {
//                 var typeString = request.TransactionType.ToString();
//                 combinedQuery = combinedQuery.Where(t => t.Type == typeString);
//             }

//             if (!string.IsNullOrWhiteSpace(request.WalletRegister))
//                 combinedQuery = combinedQuery.Where(t =>
//                     t.WalletName.Contains(request.WalletRegister)
//                 );

//             if (!string.IsNullOrWhiteSpace(request.ExpenseCategory))
//                 combinedQuery = combinedQuery.Where(t =>
//                     t.ExpenseCategory != null && t.ExpenseCategory.Contains(request.ExpenseCategory)
//                 );

//             if (!string.IsNullOrWhiteSpace(request.PerformedBy))
//                 combinedQuery = combinedQuery.Where(t =>
//                     t.PerformedBy.Contains(request.PerformedBy)
//                 );

//             if (request.FromDate != null)
//                 combinedQuery = combinedQuery.Where(t => t.Date >= request.FromDate);

//             if (request.ToDate != null)
//                 combinedQuery = combinedQuery.Where(t => t.Date <= request.ToDate);

//             var finalQuery = combinedQuery.Select(x => new GetCashTransactionsHistoryResponse
//             {
//                 Id = x.Id,
//                 TransactionType = x.Type,
//                 WalletRegister = x.WalletName,
//                 Notes = x.Notes ?? (x.Type == "Incoming" ? "Sale recorded" : "Expense recorded"),
//                 Amount = x.Amount,
//                 PerformedBy = x.PerformedBy,
//                 CustomerName = x.CustomerName,
//                 ExpenseCategory = x.ExpenseCategory,
//                 Date = x.Date,
//             });

//             var count = await finalQuery.CountAsync(cancellationToken);

//             var response = await finalQuery
//                 .OrderByDescending(t => t.Date)
//                 .Paginate(request)
//                 .ToListAsync(cancellationToken);

//             return Result<PaginationResponse<GetCashTransactionsHistoryResponse>>.Success(
//                 new PaginationResponse<GetCashTransactionsHistoryResponse>
//                 {
//                     Data = response,
//                     Count = count,
//                 }
//             );
//         }
//     }
// }
