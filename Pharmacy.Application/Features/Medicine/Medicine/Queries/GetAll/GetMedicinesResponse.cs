using Pharmacy.Application.Dto.Common.Queries;

namespace Pharmacy.Application.Features.Medicine.Medicine.Queries.GetAll;

public class GetMedicinesResponse : CommonQueryResponseBase
{
    public required string Barcode { get; set; }  
}
