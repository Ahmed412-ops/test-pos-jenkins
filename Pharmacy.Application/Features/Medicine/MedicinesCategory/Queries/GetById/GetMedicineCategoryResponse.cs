using Pharmacy.Application.Dto.Common.Queries;

namespace Pharmacy.Application.Features.Medicine.MedicinesCategory.Queries.GetById;

public class GetMedicineCategoryResponse : CommonQueryResponseBase
{
    public required string Level { get; set; }
    public Guid? ParentCategoryId { get; set; }
}
