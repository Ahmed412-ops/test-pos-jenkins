using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Food.Queries.GetAll;

public class GetFoodQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetFoodQuery, Result<PaginationResponse<GetFoodResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Food.Food> _foodRepo = unitOfWork.GetRepository<Domain.Entities.Food.Food>();

    public override async Task<Result<PaginationResponse<GetFoodResponse>>> Handle(
        GetFoodQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _foodRepo.GetAllQueryableAsync(d=>!d.Is_Deleted);
        if(!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(a => a.Name.Contains(request.Name));

        var count = await query.CountAsync(cancellationToken);

        var response = query
            .Select(a => mapper.Map<GetFoodResponse>(a))
            .Paginate(request)
            .ToList();

        return Result<PaginationResponse<GetFoodResponse>>.Success(
            new PaginationResponse<GetFoodResponse> { Data = response, Count = count }
        );
    }
}
