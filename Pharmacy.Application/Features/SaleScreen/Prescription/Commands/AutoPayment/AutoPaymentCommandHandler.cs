using MediatR;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Application.Services.Abstraction.AutoPaymentService;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SaleScreen.Prescription.Commands.AutoPayment;

public class AutoPaymentCommandHandler(IAutoPaymentService paymentService, IUnitOfWork unitOfWork)
    : BaseHandler<AutoPaymentCommand, Result<AutoPaymentResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Customers.Customer> _customerRepository =
        unitOfWork.GetRepository<Domain.Entities.Customers.Customer>();

    public override async Task<Result<AutoPaymentResponse>> Handle(
        AutoPaymentCommand request,
        CancellationToken cancellationToken
    )
    {
        var customer = await _customerRepository.FindAsync(c => c.Id == request.CustomerId);

        if (customer == null)
            return Result<AutoPaymentResponse>.Fail(Messages.CustomerNotFound);

        var result = await paymentService.DistributePaymentAsync(
            request.CustomerId,
            request.ShiftWalletId,
            request.AmountPaid
        );

        return Result<AutoPaymentResponse>.Success(
            new AutoPaymentResponse
            {
                TotalAmountApplied = result.AppliedPayments.Sum(p => p.AmountPaid),
                CreditBalance = result.RemainingCredit,
                AppliedPayments = result
                    .AppliedPayments.Select(p => new PaymentDetailDto
                    {
                        PrescriptionId = p.PrescriptionId,
                        AppliedAmount = p.AmountPaid,
                        RemainingBalance = p.RemainingBalance,
                    })
                    .ToList(),
            }
        );
    }
}
