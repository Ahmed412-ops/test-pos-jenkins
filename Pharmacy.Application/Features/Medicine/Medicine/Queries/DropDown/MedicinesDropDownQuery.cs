using MediatR;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Medicine.Medicine.Queries.DropDown;

public class MedicinesDropDownQuery : IRequest<Result<List<DropDownQueryResponse>>>
{
    
}
