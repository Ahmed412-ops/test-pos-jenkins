using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Entities.Stock;

namespace Pharmacy.Application.Features.SaleScreen.Prescription.Commands.Update;

public class UpdateprescriptionCommandValidator : AbstractValidator<UpdatePrescriptionCommand>
{
    public UpdateprescriptionCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.PrescriptionId)
        .MustExistPrescription(unitOfWork);

        RuleFor(x => x.CustomerId).MustExistCustomer(unitOfWork).When(x => x.CustomerId.HasValue);
        RuleFor(x => x.ShiftId).NotEmpty().MustExistShift(unitOfWork);

        RuleForEach(x => x.Payments)
            .ChildRules(x =>
            {
                x.RuleFor(p => p.ShiftWalletId).NotEmpty().MustExistShiftWallet(unitOfWork);
                x.RuleFor(p => p.Amount).GreaterThan(0);
            });
        When(
            x => !x.CustomerId.HasValue,
            () =>
            {
                RuleFor(x => x.CashbackUsed)
                    .Equal(0m)
                    .WithMessage(Messages.CashbackCannotBeUsedForNonRegisteredCustomers);

                RuleFor(x => x.CreditUsed)
                    .Equal(0m)
                    .WithMessage(Messages.CreditCannotBeUsedForNonRegisteredCustomers);

                RuleFor(x => x.Payments)
                    .NotEmpty()
                    .WithMessage(Messages.PaymentsRequiredForNonRegisteredCustomers);
                RuleFor(x => x)
                    .MustAsync(
                        async (command, cancellation) =>
                        {
                            var stockIds = command
                                .PrescriptionItems.Select(i => i.MedicationStockId)
                                .Distinct()
                                .ToList();

                            var stocks = await unitOfWork
                                .GetRepository<MedicationStock>()
                                .GetAllAsync(s => stockIds.Contains(s.Id));

                            var stockDict = stocks.ToDictionary(s => s.Id);
                            decimal totalRequired = command.PrescriptionItems.Sum(item =>
                            {
                                var stock = stockDict[item.MedicationStockId];
                                return (stock.SellingPrice - item.AppliedDiscount) * item.Quantity;
                            });

                            decimal totalPaid = command.Payments.Sum(p => p.Amount);
                            return totalPaid == totalRequired;
                        }
                    )
                    .WithMessage(Messages.PaymentsMustEqualTotalCost);
            }
        );

        RuleForEach(x => x.PrescriptionItems)
            .SetValidator(new UpdatePrescriptionItemDtoValidator(unitOfWork));
    }
}