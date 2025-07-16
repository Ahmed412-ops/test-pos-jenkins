using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Features.Customer.Queries.GetById.GetByIdResponses;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Customer.Queries.GetById;

public class GetCustomerByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetCustomerByIdQuery, Result<GetCustomerByIdResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Customers.Customer> _customerRepo =
        unitOfWork.GetRepository<Domain.Entities.Customers.Customer>();

    public override async Task<Result<GetCustomerByIdResponse>> Handle(
        GetCustomerByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var customer = await _customerRepo.FindAsync(
            c => c.Id == request.Id,
            Include: c =>
                c.Include(c => c.Addresses)
                    .Include(c => c.PhoneNumbers)
                    .Include(c => c.CustomerChronicMedicines)
                        .ThenInclude(cm => cm.Medicine)
                    .Include(c => c.CustomerChronicDiseases)
                        .ThenInclude(cd => cd.Disease)
                    .Include(c => c.Prescriptions)
                        .ThenInclude(p => p.Items)
                            .ThenInclude(i => i.MedicationStock)
                                .ThenInclude(ms => ms.Medicine)
                    .Include(c => c.Prescriptions)
                        .ThenInclude(p => p.Transactions)
                    .Include(c => c.BalanceTransactions)
        );

        if (customer is null)
            return Result<GetCustomerByIdResponse>.Fail(Messages.CustomerNotFound);

        var response = mapper.Map<GetCustomerByIdResponse>(customer);

        return Result<GetCustomerByIdResponse>.Success(response);
    }
}
