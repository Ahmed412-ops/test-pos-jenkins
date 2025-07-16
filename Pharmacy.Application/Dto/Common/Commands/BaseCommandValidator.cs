using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Application.Dto.Common.Commands;

public class BaseCommandValidator<TCommand, TEntity> : AbstractValidator<TCommand>
    where TCommand : IBaseCommand
    where TEntity : EntityModel
{
    public BaseCommandValidator(IUnitOfWork unitOfWork, bool checkName = true)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        if (checkName)
            ConfigureNameRule(unitOfWork);

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage(Messages.DescriptionIsTooLong);
    }
    private void ConfigureNameRule(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(Messages.NameIsRequired)
            .MaximumLength(100).WithMessage(Messages.NameIsTooLong)
            .MustAsync(async (command, name, cancellation) =>
            {
                bool exists = await unitOfWork.GetRepository<TEntity>()
                    .IsExistsAsync(x => x.Name == name);
                return !exists;
            })
            .WithMessage(Messages.NameAlreadyExists);
    }
}
