using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Features;
using Pharmacy.Application.Features.Customer.Commands.Create;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Customers;

public class CreateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<CreateCustomerCommand, Result<CreateCustomerResponse>>
{
    private readonly IGenericRepository<Customer> _customerRepository = unitOfWork.GetRepository<Customer>();

    public override async Task<Result<CreateCustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
            var customer = mapper.Map<Customer>(request);
            await _customerRepository.AddAsync(customer);
            await unitOfWork.SaveChangesAsync();
        return Result<CreateCustomerResponse>.Success(new CreateCustomerResponse
        {
            Id = customer.Id
        } , Messages.SuccessfullyCreated);

    }
}
