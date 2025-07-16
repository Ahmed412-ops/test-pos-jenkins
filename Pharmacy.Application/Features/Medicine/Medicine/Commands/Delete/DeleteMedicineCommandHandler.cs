using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Medicine.Medicine.Commands.Delete;

public class DeleteMedicineCommandHandler(IUnitOfWork unitOfWork)
    : BaseHandler<DeleteMedicineCommand, Result<bool>>
{
    private readonly IGenericRepository<Domain.Entities.Medicine.Medicine> _medicineRepository =
        unitOfWork.GetRepository<Domain.Entities.Medicine.Medicine>();

    public override async Task<Result<bool>> Handle(
        DeleteMedicineCommand request,
        CancellationToken cancellationToken
    )
    {
        var medicine = await _medicineRepository.FindAsync(
            m => m.Id == request.Id,
            Include: m => m.Include(m => m.MedicineUnits)
                            .Include(m => m.EffectiveMaterials)
                            .Include(m => m.CrossSellingRecommendations));
        
        if (medicine == null)
            return Result<bool>.Fail(Messages.MedicineNotFound);
        
        if(
            medicine.MedicineUnits.Count != 0
            || medicine.EffectiveMaterials.Count != 0
            || medicine.CrossSellingRecommendations.Count != 0
        )
            return Result<bool>.Fail(Messages.RelationExists);

        medicine.Is_Deleted = true;

        int result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        return Result<bool>.Success(true, Messages.DeletedSuccessfully);
    }
}
