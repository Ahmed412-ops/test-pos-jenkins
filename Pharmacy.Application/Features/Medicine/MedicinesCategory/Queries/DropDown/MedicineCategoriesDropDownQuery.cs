using MediatR;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Medicine.MedicinesCategory.Queries.DropDown;

public class MedicineCategoriesDropDownQuery : IRequest<Result<List<MedicineCategoriesDropDownResponse>>>
{
    
}
