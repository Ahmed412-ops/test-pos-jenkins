using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Features.Stock.Medication.Commands.Create;

namespace Pharmacy.Application.Features.Stock.Medication.Commands.Update;

public class UpdateMedicationStockCommandValidator : AbstractValidator<UpdateMedicationStockCommand>
{
    public UpdateMedicationStockCommandValidator(IUnitOfWork unitOfWork)
    {
        Include(new CreateMedicationStockCommandValidator(unitOfWork));
    }    
}
