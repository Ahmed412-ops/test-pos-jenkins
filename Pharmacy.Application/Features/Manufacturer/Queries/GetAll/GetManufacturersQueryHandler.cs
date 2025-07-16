using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Manufacturer.Queries.GetAll;

public class GetManufacturersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetManufacturersQuery, Result<PaginationResponse<GetManufacturersResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Manufacturers.Manufacturer> _manufacturerRepo = unitOfWork.GetRepository<Domain.Entities.Manufacturers.Manufacturer>();

    public override async Task<Result<PaginationResponse<GetManufacturersResponse>>> Handle(
        GetManufacturersQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _manufacturerRepo.GetAllQueryableAsync(d=>!d.Is_Deleted);
        if(!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(a => a.Name.Contains(request.Name));

        var count = await query.CountAsync(cancellationToken);

        var response = query
            .Select(a => mapper.Map<GetManufacturersResponse>(a))
            .Paginate(request)
            .ToList();

        return Result<PaginationResponse<GetManufacturersResponse>>.Success(
            new PaginationResponse<GetManufacturersResponse> { Data = response, Count = count }
        );
    }
}
