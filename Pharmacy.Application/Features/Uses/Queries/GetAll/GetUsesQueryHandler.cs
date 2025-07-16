using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Uses.Queries.GetAll;

public class GetUsesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<GetUsesQuery, Result<PaginationResponse<GetUsesResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Uses.Use> _useRepo = unitOfWork.GetRepository<Domain.Entities.Uses.Use>();

    public override async Task<Result<PaginationResponse<GetUsesResponse>>> Handle(
        GetUsesQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _useRepo.GetAllQueryableAsync(d => !d.Is_Deleted);
        if (!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(a => a.Name.Contains(request.Name));

        var count = await query.CountAsync(cancellationToken);

        var response = query
            .Select(a => mapper.Map<GetUsesResponse>(a))
            .Paginate(request)
            .ToList();

        return Result<PaginationResponse<GetUsesResponse>>.Success(
            new PaginationResponse<GetUsesResponse> { Data = response, Count = count }
        );
    }
}

