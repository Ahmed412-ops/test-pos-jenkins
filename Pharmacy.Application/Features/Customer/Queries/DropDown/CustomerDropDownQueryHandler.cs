using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Customer.Queries.DropDown;

class CustomerDropDownQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<CustomerDropDownQuery, Result<List<DropDownQueryResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Customers.Customer> _customerRepo = unitOfWork.GetRepository<Domain.Entities.Customers.Customer>();
    public override async Task<Result<List<DropDownQueryResponse>>> Handle
        (CustomerDropDownQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepo.GetAllAsync(d => !d.Is_Deleted);
        var result = mapper.Map<List<DropDownQueryResponse>>(customer);
        return Result<List<DropDownQueryResponse>>.Success(result);
    }
}
