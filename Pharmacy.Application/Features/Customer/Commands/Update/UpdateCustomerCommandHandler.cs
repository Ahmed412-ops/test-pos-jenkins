using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Features.Customer.Commands.Create;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Customer.Commands.Update;

public class UpdateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) :
    BaseHandler<UpdateCustomerCommand, Result<CreateCustomerResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Customers.Customer> _customerRepository = unitOfWork.GetRepository<Domain.Entities.Customers.Customer>();

    public override async Task<Result<CreateCustomerResponse>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.FindAsync(
            a => a.Id == request.Id,
            Include: c => c
                .Include(c => c.Addresses)
                .Include(c => c.PhoneNumbers)
                .Include(c => c.CustomerChronicDiseases)
                .Include(c => c.CustomerChronicMedicines)
        ); 
        
        if (customer == null)
            return Result<CreateCustomerResponse>.Fail(Messages.CustomerNotFound);


        customer.Addresses.Clear();
        customer.PhoneNumbers.Clear();
        customer.CustomerChronicDiseases.Clear();
        customer.CustomerChronicMedicines.Clear();

        mapper.Map(request, customer);

        await _customerRepository.SaveChangesAsync();
        return Result<CreateCustomerResponse>.Success(Messages.SuccessfullyUpdated);
    }
}
