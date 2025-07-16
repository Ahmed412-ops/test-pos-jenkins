using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Features.Customer.Queries.GetById.GetByIdResponses;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Customer.Queries.GetById;

public class GetCustomerByIdQuery : IRequest<Result<GetCustomerByIdResponse>>
{
    public Guid Id { get; set; }
}

