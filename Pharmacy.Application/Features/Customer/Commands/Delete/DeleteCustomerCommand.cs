using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Customer.Commands.Delete;

public record DeleteCustomerCommand(Guid Id) : IRequest<Result<bool>>;

