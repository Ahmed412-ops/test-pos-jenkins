using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Queries.GetAll;

public class GetSupplierInvoicesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) 
    : BaseHandler<GetSupplierInvoicesQuery, Result<InvoicesPaginationResponse>>
{
    private readonly IGenericRepository<Domain.Entities.SupplierInvoice.SupplierInvoice> _supplierInvoiceRepo = unitOfWork.GetRepository<Domain.Entities.SupplierInvoice.SupplierInvoice>();
    public override async Task<Result<InvoicesPaginationResponse>> Handle(
        GetSupplierInvoicesQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _supplierInvoiceRepo.GetAllQueryableAsync(d=>!d.Is_Deleted,
                                     Include: a => a.Include(b => b.Supplier));

        if(!string.IsNullOrWhiteSpace(request.InvoiceNumber))
            query = query.Where(a => a.InvoiceNumber == request.InvoiceNumber);
        
        if(!string.IsNullOrWhiteSpace(request.SupplierName))
            query = query.Where(a => a.Supplier.Name == request.SupplierName);
        
        if (request.InvoiceStartDate.HasValue)
            query = query.Where(a => a.InvoiceDate >= request.InvoiceStartDate);

        if (request.InvoiceEndDate.HasValue)
            query = query.Where(a => a.InvoiceDate <= request.InvoiceEndDate);

        if (request.DueStartDate.HasValue)
            query = query.Where(a => a.DueDate >= request.DueStartDate);

        if (request.DueEndDate.HasValue)
            query = query.Where(a => a.DueDate <= request.DueEndDate);
        
        if (request.IsReviewed.HasValue)
            query = query.Where(a => a.IsReviewed == request.IsReviewed);
        if (request.IsReceived.HasValue)
            query = query.Where(a => a.IsRecevied == request.IsReceived);

        query = request.PaymentStatus switch
        {
            PaymentStatus.Unpaid => query.Where(a => a.PaymentStatus == PaymentStatus.Unpaid),
            PaymentStatus.Paid => query.Where(a => a.PaymentStatus == PaymentStatus.Paid),
            PaymentStatus.PartiallyPaid => query.Where(a => a.PaymentStatus == PaymentStatus.PartiallyPaid),
            _ => query
        };

        var count = await query.CountAsync(cancellationToken);
        var totalFinal = await query.SumAsync(a => a.FinalInvoiceTotal, cancellationToken);
        var totalPaid = await query.SumAsync(a => a.AmountPaid, cancellationToken);
        var totalRemaining = totalFinal - totalPaid;

        var response = query
            .Select(a => mapper.Map<GetSupplierInvoicesResponse>(a))
            .Paginate(request)
            .ToList();
        
        var invoicesPaginationResponse = new InvoicesPaginationResponse
        {
            Data = response,
            Count = count,
            TotalRemaining = totalRemaining
        };

        return Result<InvoicesPaginationResponse>.Success(invoicesPaginationResponse);
    }
}
