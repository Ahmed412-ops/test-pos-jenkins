using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Supplier.Queries.DropDown;

public class SuppliersDropDownQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<SuppliersDropDownQuery, Result<List<DropDownQueryResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Supplier.Supplier> _supplierRepo = unitOfWork.GetRepository<Domain.Entities.Supplier.Supplier>();

    public override async Task<Result<List<DropDownQueryResponse>>> Handle(
        SuppliersDropDownQuery request,
        CancellationToken cancellationToken)
    {
        var suppliers = await _supplierRepo.GetAllAsync(d => !d.Is_Deleted);
        var result = mapper.Map<List<DropDownQueryResponse>>(suppliers);
        return Result<List<DropDownQueryResponse>>.Success(result);
    }
}

