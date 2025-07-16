using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Supplier.Queries.GetById;

public class GetSupplierQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<GetSupplierQuery, Result<GetSupplierResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Supplier.Supplier> _supplierRepo = unitOfWork.GetRepository<Domain.Entities.Supplier.Supplier>();


    public override async Task<Result<GetSupplierResponse>> Handle(GetSupplierQuery request, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepo.FindAsync(
            s => s.Id == request.Id,
            Include: s => s.Include(c => c.Contacts),
            asNoTracking: true);

        if (supplier == null)
            return Result<GetSupplierResponse>.Fail(Messages.NotFound);

        var response = mapper.Map<GetSupplierResponse>(supplier);

        return Result<GetSupplierResponse>.Success(response);
    }
}
