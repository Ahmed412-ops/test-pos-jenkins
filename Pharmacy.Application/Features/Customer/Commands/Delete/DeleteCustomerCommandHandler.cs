using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Customer.Commands.Delete;

public class DeleteCustomerCommandHandler(IUnitOfWork unitOfWork)
    : BaseHandler<DeleteCustomerCommand, Result<bool>>
{
    private readonly IGenericRepository<Domain.Entities.Customers.Customer> _customerRepository = unitOfWork.GetRepository<Domain.Entities.Customers.Customer>();
    public override async Task<Result<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.FindAsync(c => c.Id == request.Id);
        if (customer == null)
            return Result<bool>.Fail(Messages.NotFound);
        customer.Is_Deleted = true;
        int result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<bool>.Fail(Messages.SomethingWentWrong);
        return Result<bool>.Success(true, Messages.DeletedSuccessfully);
    }
}
