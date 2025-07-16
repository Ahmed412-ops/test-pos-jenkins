using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Medicine.Medicine.Queries.DropDown;

public class MedicinesDropDownQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<MedicinesDropDownQuery, Result<List<DropDownQueryResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Medicine.Medicine> _medicineRepo = unitOfWork.GetRepository<Domain.Entities.Medicine.Medicine>();

    public override async Task<Result<List<DropDownQueryResponse>>> Handle(
        MedicinesDropDownQuery request,
        CancellationToken cancellationToken)
    {
        var medicines = await _medicineRepo.GetAllAsync(d => !d.Is_Deleted);
        var result = mapper.Map<List<DropDownQueryResponse>>(medicines);
        return Result<List<DropDownQueryResponse>>.Success(result);
    }
}
