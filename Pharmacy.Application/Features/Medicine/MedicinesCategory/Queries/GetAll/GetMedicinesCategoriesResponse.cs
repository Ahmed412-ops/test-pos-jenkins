using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Medicine.MedicinesCategory.Queries.GetAll;

public class GetMedicineCategoriesResponse : CommonQueryResponseBase
{
    public required string Level { get; set; }
}
