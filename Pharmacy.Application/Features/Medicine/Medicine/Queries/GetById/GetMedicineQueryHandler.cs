using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Medicine.Medicine.Queries.GetById;

public class GetMedicineQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetMedicineQuery, Result<GetMedicineResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Medicine.Medicine> _medicineRepo = unitOfWork.GetRepository<Domain.Entities.Medicine.Medicine>();

    public override async Task<Result<GetMedicineResponse>> Handle(GetMedicineQuery request, CancellationToken cancellationToken)
    {
        var medicine = await _medicineRepo.FindAsync(
            m => m.Id == request.Id,
            Include: m => m.Include(m => m.MedicineCategory)
                            .Include(m => m.Manufacturer)
                            .Include(m => m.DosageForm)
                            .Include(m => m.MedicineUnits)
                            .ThenInclude(m => m.Unit)
                            .Include(m => m.EffectiveMaterials)
                            .ThenInclude(m => m.EffectiveMaterial!)
                            .Include(m => m.CrossSellingRecommendations));

        if (medicine == null)
            return Result<GetMedicineResponse>.Fail(Messages.MedicineNotFound);

        var response = mapper.Map<GetMedicineResponse>(medicine);

        return Result<GetMedicineResponse>.Success(response);
    }
}
