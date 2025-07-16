using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Queries.GetTransactions;

public class GetTransactionsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetTransactionsQuery, Result<PaginationResponse<GetTransactionsResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.SupplierTransaction.SupplierTransaction> _transactionRepo = unitOfWork.GetRepository<Domain.Entities.SupplierTransaction.SupplierTransaction>();

    public override async Task<Result<PaginationResponse<GetTransactionsResponse>>> Handle(
        GetTransactionsQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _transactionRepo.GetAllQueryableAsync(d => d.SupplierInvoiceId == request.InvoiceId,
                                     Include: a => a .Include(b => b.SupplierInvoice!)
                                                        .ThenInclude(c => c.Supplier!));

        var Count = await query.CountAsync();

        var response = await query
            .Select(a => mapper.Map<GetTransactionsResponse>(a))
            .Paginate(request)
            .ToListAsync();

        return Result<PaginationResponse<GetTransactionsResponse>>.Success(new PaginationResponse<GetTransactionsResponse>
        {
            Data = response,
            Count = Count
        });
    }
}
