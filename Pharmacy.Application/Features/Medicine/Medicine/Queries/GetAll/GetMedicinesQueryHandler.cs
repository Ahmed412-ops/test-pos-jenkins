using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Medicine.Medicine.Queries.GetAll;

public class GetMedicinesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetMedicinesQuery, Result<PaginationResponse<GetMedicinesResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Medicine.Medicine> _medicineRepo = unitOfWork.GetRepository<Domain.Entities.Medicine.Medicine>();

    public override async Task<Result<PaginationResponse<GetMedicinesResponse>>> Handle(
        GetMedicinesQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _medicineRepo.GetAllQueryableAsync(d => !d.Is_Deleted);
        if(!string.IsNullOrWhiteSpace(request.Barcode))
            query = query.Where(a => a.Barcode.Contains(request.Barcode));

        if(!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(a => a.Name.Contains(request.Name));

        var count = await query.CountAsync(cancellationToken);

        var response = query
            .Select(a => mapper.Map<GetMedicinesResponse>(a))
            .Paginate(request)
            .ToList();

        return Result<PaginationResponse<GetMedicinesResponse>>.Success(
            new PaginationResponse<GetMedicinesResponse> { Data = response, Count = count }
        );
    }
}
