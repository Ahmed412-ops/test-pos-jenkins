namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.Create;

using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Application.Resources.Static;

public class CreateSupplierInvoiceItemDtoValidator : AbstractValidator<CreateSupplierInvoiceItemDto>
{
    public CreateSupplierInvoiceItemDtoValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(i => i.MedicineUnitId)
            .NotEmpty()
            .MustExistMedicineUnit(unitOfWork);
        
        RuleFor(i => i.Quantity)
            .GreaterThan(0)
            .WithMessage(Messages.QuantityMustBeGreaterThanZero);
        
        RuleFor(x => x.PublicSellingPrice)
            .GreaterThan(0).WithMessage(Messages.PublicSellingPriceMustBeGreaterThanZero);
        
        RuleFor(x => x.TaxAmount)
            .GreaterThanOrEqualTo(0).WithMessage(Messages.TaxPercentageMustBeGreaterThanOrEqualToZero);
        
        RuleFor(x => x.ExpiryDate)
            .NotEmpty().WithMessage(Messages.ExpiryDateRequired);
    }
}

