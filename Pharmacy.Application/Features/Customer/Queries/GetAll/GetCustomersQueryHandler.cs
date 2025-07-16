using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Customer.Queries.GetAll;

public class GetCustomersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetCustomersQuery, Result<PaginationResponse<GetCustomersResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Customers.Customer> _customerRepository = unitOfWork.GetRepository<Domain.Entities.Customers.Customer>();

    public override async Task<Result<PaginationResponse<GetCustomersResponse>>> Handle
        (GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var query = await _customerRepository.GetAllQueryableAsync(c => !c.Is_Deleted
              );

        if (!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(c => c.Name.Contains(request.Name));

        var count = await query.CountAsync(cancellationToken);

        var response = await query
            .Select(c => mapper.Map<GetCustomersResponse>(c))
            .Paginate(request)
            .ToListAsync(cancellationToken); 
        
            return Result<PaginationResponse<GetCustomersResponse>>.Success(
            new PaginationResponse<GetCustomersResponse> { Data = response, Count = count }
        );
    }
}
