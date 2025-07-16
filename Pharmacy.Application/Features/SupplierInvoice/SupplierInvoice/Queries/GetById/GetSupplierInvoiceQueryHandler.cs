using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Queries.GetById;

public class GetSupplierInvoiceQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<GetSupplierInvoiceQuery, Result<GetSupplierInvoiceResponse>>
{
    private readonly IGenericRepository<Domain.Entities.SupplierInvoice.SupplierInvoice> _supplierInvoiceRepo = unitOfWork.GetRepository<Domain.Entities.SupplierInvoice.SupplierInvoice>();


    public override async Task<Result<GetSupplierInvoiceResponse>> Handle(GetSupplierInvoiceQuery request, CancellationToken cancellationToken)
    {
        var supplierInvoice = await _supplierInvoiceRepo.FindAsync(
            s => s.Id == request.Id,
            Include: s => s.Include(i => i.Supplier)
                            .Include(i => i.InvoiceItems)
                            .ThenInclude(m => m.MedicineUnit)
                            .ThenInclude(m => m.Medicine)
                            .Include(i => i.InvoiceItems)
                            .ThenInclude(m => m.MedicineUnit)
                            .ThenInclude(m => m.Unit)
                            .Include(i => i.PurchaseOrder!),
            asNoTracking: true);

        if (supplierInvoice == null)
            return Result<GetSupplierInvoiceResponse>.Fail(Messages.NotFound);

        var response = mapper.Map<GetSupplierInvoiceResponse>(supplierInvoice);

        return Result<GetSupplierInvoiceResponse>.Success(response);
    }
}
