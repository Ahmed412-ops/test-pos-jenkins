using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Helper.Extensions;

namespace Pharmacy.Application.Features.Disease.Disease.Commands.Create;

public class CreateDiseaseCommandValidator
    : BaseCommandValidator<CreateDiseaseCommand, Domain.Entities.Disease.Disease>
{
    public CreateDiseaseCommandValidator(IUnitOfWork context, bool checkName = true)
        : base(context, checkName)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.DiseaseCategoryId).MustExistDiseaseCategory(context);

        RuleForEach(x => x.Symptoms).MustExistSymptom(context);
    }
}
