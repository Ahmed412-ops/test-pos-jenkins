using MediatR;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Features.Medicine.MedicinesCategory.Commands.Create;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Medicine.MedicinesCategory.Commands.Update;

public class UpdateMedicineCategoryCommand : CreateMedicineCategoryCommand, IBaseUpdateCommand, IRequest<Result<string>>
{
    public Guid Id { get; set; }
}
