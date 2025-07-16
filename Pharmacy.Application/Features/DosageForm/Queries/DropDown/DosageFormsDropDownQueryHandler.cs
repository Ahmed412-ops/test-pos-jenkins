using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.DosageForm.Queries.DropDown;

public class DosageFormsDropDownQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<DosageFormsDropDownQuery, Result<List<DropDownQueryResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.DosageForm.DosageForm> _dosageFormRepo = unitOfWork.GetRepository<Domain.Entities.DosageForm.DosageForm>();

    public override async Task<Result<List<DropDownQueryResponse>>> Handle(
        DosageFormsDropDownQuery request,
        CancellationToken cancellationToken)
    {
        var dosageForms = await _dosageFormRepo.GetAllAsync(d => !d.Is_Deleted);
        var result = mapper.Map<List<DropDownQueryResponse>>(dosageForms);
        return Result<List<DropDownQueryResponse>>.Success(result);
    }
}
