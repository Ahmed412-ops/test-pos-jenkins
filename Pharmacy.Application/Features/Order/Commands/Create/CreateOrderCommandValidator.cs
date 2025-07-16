using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Application.Resources.Static;

namespace Pharmacy.Application.Features.Order.Commands.Create;

public class CreateOrderCommandValidator : BaseCommandValidator<CreateOrderCommand, Domain.Entities.Order.PurchaseOrder>
{
    public CreateOrderCommandValidator(IUnitOfWork unitOfWork, bool checkName = true)
        : base(unitOfWork, checkName)
    {
        RuleFor(x => x.SupplierId)
            .NotEmpty()
            .MustExistSupplier(unitOfWork);

        RuleFor(x => x.OrderDate)
            .NotEmpty()
            .WithMessage(Messages.OrderDateIsRequired);

        RuleForEach(x => x.Items)
            .ChildRules(item =>
            {
                item.RuleFor(i => i.MedicineUnitId)
                    .NotEmpty()
                    .MustExistMedicineUnit(unitOfWork);

                item.RuleFor(i => i.Quantity)
                    .GreaterThan(0)
                    .WithMessage(Messages.QuantityMustBeGreaterThanZero);
            });
    }
}
