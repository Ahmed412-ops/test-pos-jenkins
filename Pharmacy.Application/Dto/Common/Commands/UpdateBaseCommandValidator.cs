using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Application.Dto.Common.Commands;

public class UpdateBaseCommandValidator<TCommand, TEntity> : AbstractValidator<TCommand>
    where TCommand : IBaseUpdateCommand
    where TEntity : EntityModel
{
    public UpdateBaseCommandValidator(IUnitOfWork unitOfWork)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(Messages.IdIsRequired)
            .MustAsync(async (id, cancellation) =>
            {
                return await unitOfWork.GetRepository<TEntity>().IsExistsAsync(x => x.Id == id);
            })
            .WithMessage(Messages.NotFound);

        ConfigureNameRule(unitOfWork);
    }

    protected void ConfigureNameRule(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(Messages.NameIsRequired)
            .MaximumLength(100).WithMessage(Messages.NameIsTooLong);

        RuleFor(x => x)
            .CustomAsync(async (command, context, cancellation) =>
            {
                var repo = unitOfWork.GetRepository<TEntity>();
                var existingEntity = await repo.FindAsync(d => d.Id == command.Id);

                if (!string.Equals(existingEntity!.Name, command.Name, StringComparison.OrdinalIgnoreCase))
                {
                    bool nameExists = await repo.IsExistsAsync(x => x.Name == command.Name);
                    if (nameExists)
                        context.AddFailure(Messages.NameAlreadyExists);
                }
            });
    }
}
