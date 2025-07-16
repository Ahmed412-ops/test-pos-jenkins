using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.DosageForm.Queries.GetAll;

public class GetDosageFormsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
  : BaseHandler<GetDosageFormsQuery, Result<PaginationResponse<GetDosageFormsResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.DosageForm.DosageForm> _dosageFormRepo = unitOfWork.GetRepository<Domain.Entities.DosageForm.DosageForm>();

    public override async Task<Result<PaginationResponse<GetDosageFormsResponse>>> Handle(
        GetDosageFormsQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _dosageFormRepo.GetAllQueryableAsync(d=>!d.Is_Deleted);
        if(!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(a => a.Name.Contains(request.Name));

        var count = await query.CountAsync(cancellationToken);

        var response = query
            .Select(a => mapper.Map<GetDosageFormsResponse>(a))
            .Paginate(request)
            .ToList();

        return Result<PaginationResponse<GetDosageFormsResponse>>.Success(
            new PaginationResponse<GetDosageFormsResponse> { Data = response, Count = count }
        );
    }
}
