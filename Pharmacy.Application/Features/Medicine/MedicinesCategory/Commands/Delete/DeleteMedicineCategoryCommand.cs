using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Medicine.MedicinesCategory.Commands.Delete;

public class DeleteMedicineCategoryCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}
