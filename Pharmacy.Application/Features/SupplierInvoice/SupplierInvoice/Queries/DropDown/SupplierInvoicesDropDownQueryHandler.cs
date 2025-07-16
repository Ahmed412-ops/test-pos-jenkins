using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Queries.DropDown;

public class SupplierInvoicesDropDownQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<SupplierInvoicesDropDownQuery, Result<List<DropDownQueryResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.SupplierInvoice.SupplierInvoice> _supplierInvoiceRepo = unitOfWork.GetRepository<Domain.Entities.SupplierInvoice.SupplierInvoice>();
    public override async Task<Result<List<DropDownQueryResponse>>> Handle(
        SupplierInvoicesDropDownQuery request,
        CancellationToken cancellationToken)
    {
        var supplierInvoices = await _supplierInvoiceRepo.GetAllAsync(d => !d.Is_Deleted);
        var result = mapper.Map<List<DropDownQueryResponse>>(supplierInvoices);
        return Result<List<DropDownQueryResponse>>.Success(result);
    }
}
