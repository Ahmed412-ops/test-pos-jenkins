using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Manufacturer.Queries.DropDown;

public class ManufacturersDropDownQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<ManufacturersDropDownQuery, Result<List<DropDownQueryResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Manufacturers.Manufacturer> _manufacturerRepo = unitOfWork.GetRepository<Domain.Entities.Manufacturers.Manufacturer>();

    public override async Task<Result<List<DropDownQueryResponse>>> Handle(
        ManufacturersDropDownQuery request,
        CancellationToken cancellationToken)
    {
        var manufacturers = await _manufacturerRepo.GetAllAsync(d => !d.Is_Deleted);
        var result = mapper.Map<List<DropDownQueryResponse>>(manufacturers);
        return Result<List<DropDownQueryResponse>>.Success(result);
    }
}
