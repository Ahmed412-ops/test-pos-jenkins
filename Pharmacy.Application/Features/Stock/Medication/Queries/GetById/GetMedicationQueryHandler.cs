using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.Medication.Queries.GetById;

public class GetMedicationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetMedicationQuery, Result<GetMedicationResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Stock.MedicationStock> _medicationRepo = unitOfWork.GetRepository<Domain.Entities.Stock.MedicationStock>();
    public override async Task<Result<GetMedicationResponse>> Handle(
        GetMedicationQuery request,
        CancellationToken cancellationToken
    )
    {
        var medication = await _medicationRepo.FindAsync(m => m.Id == request.Id,
                                            Include: source => source
                                                .Include(m => m.Medicine));

        if (medication == null)
            return Result<GetMedicationResponse>.Fail(Messages.NotFound);
        
        var response = mapper.Map<GetMedicationResponse>(medication);
        return Result<GetMedicationResponse>.Success(response);
    }
}
