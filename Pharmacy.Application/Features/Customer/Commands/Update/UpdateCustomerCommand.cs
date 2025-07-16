using MediatR;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Features.Customer.Commands.Create;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Customer.Commands.Update;

public class UpdateCustomerCommand : CreateCustomerCommand,IBaseUpdateCommand
{
    public Guid Id { get; set; }

}
