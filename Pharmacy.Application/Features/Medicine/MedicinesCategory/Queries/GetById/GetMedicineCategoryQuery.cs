using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Medicine.MedicinesCategory.Queries.GetById;

public class GetMedicineCategoryQuery : IRequest<Result<GetMedicineCategoryResponse>>
{
    public Guid Id { get; set; }
}
