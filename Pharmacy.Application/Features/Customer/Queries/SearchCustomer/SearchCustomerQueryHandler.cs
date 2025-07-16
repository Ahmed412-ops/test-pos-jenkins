using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Customer.Queries.SearchCustomer;

public class SearchCustomerQueryHandler(IUnitOfWork unitOfWork)
    : BaseHandler<SearchCustomerQuery, Result<List<SearchCustomerResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Customers.Customer> _customerRepository =
        unitOfWork.GetRepository<Domain.Entities.Customers.Customer>();

    public override async Task<Result<List<SearchCustomerResponse>>> Handle(
        SearchCustomerQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _customerRepository.GetAllAsync(
            filterPredicate: c =>
                c.PhoneNumbers.Any(p => p.Number.Contains(request.NameOrPhone!))
                || c.Name.Contains(request.NameOrPhone!),
            Include: c => c.Include(c => c.PhoneNumbers)
        );
        if (!query.Any())
            return Result<List<SearchCustomerResponse>>.Fail(Messages.CustomerNotFound);
        var response = query.Select(c => new SearchCustomerResponse
        {
            Id = c.Id,
            Name = c.Name,
            PhoneNumber = c.PhoneNumbers
                .FirstOrDefault(p => p.Number.Contains(request.NameOrPhone!))?.Number
                ?? c.PhoneNumbers.FirstOrDefault()?.Number
                ?? string.Empty
        }).ToList();
        return Result<List<SearchCustomerResponse>>.Success(response);
    }
}
