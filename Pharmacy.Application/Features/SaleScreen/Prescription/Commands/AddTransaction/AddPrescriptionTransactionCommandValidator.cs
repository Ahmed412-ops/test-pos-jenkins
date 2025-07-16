using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Application.Resources.Static;

namespace Pharmacy.Application.Features.SaleScreen.Prescription.Commands.AddTransaction;

public class AddPrescriptionTransactionCommandValidator
    : AbstractValidator<AddPrescriptionTransactionCommand>
{
    public AddPrescriptionTransactionCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.PrescriptionId).NotEmpty().MustExistPrescription(unitOfWork);

        RuleFor(x => x.ShiftWalletId).NotEmpty().MustExistShiftWallet(unitOfWork);

        RuleFor(x => x.AmountPaid).GreaterThan(0).WithMessage(Messages.AmountMustBeGreaterThanZero);
    }
}
