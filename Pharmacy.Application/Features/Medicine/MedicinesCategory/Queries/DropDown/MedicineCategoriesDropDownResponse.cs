using Pharmacy.Application.Dto.Common.Queries;

namespace Pharmacy.Application.Features.Medicine.MedicinesCategory.Queries.DropDown;

public class MedicineCategoriesDropDownResponse : DropDownQueryResponse
{
    public required string Level { get; set; }
}
