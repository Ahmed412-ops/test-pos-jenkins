using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Helper.Extensions;

namespace Pharmacy.Application.Features.SaleScreen.Prescription.Queries.CheckPrescriptionConflict;

public class CheckPrescriptionConflictValidator : AbstractValidator<CheckMedicineConflictQuery>
{
    public CheckPrescriptionConflictValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.CustomerId).MustExistCustomer(unitOfWork).When(x => x.CustomerId.HasValue);

        RuleFor(x => x.NewMedicineId).MustExistMedicine(unitOfWork);

        RuleForEach(x => x.ExistingMedicineIds).MustExistMedicine(unitOfWork);
    }
}
