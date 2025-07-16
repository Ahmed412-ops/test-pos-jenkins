using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Disease.Disease.Commands.Delete;

public class DeleteDiseaseCommandHandler(IUnitOfWork unitOfWork)
    : BaseHandler<DeleteDiseaseCommand, Result<bool>>
{
    private readonly IGenericRepository<Domain.Entities.Disease.Disease> _diseaseRepo = unitOfWork.GetRepository<Domain.Entities.Disease.Disease>();
    public override async Task<Result<bool>> Handle(DeleteDiseaseCommand request, CancellationToken cancellationToken)
    {
        var disease = await _diseaseRepo.FindAsync(
            d => d.Id == request.Id,
            Include: d => d.Include(s => s.Symptoms)
                        .Include(s => s.EffectiveMaterialDiseases));
        
        if (disease == null)
            return Result<bool>.Fail(Messages.NotFound);
        
        if (disease.Symptoms.Count != 0 || disease.EffectiveMaterialDiseases.Count != 0)
            return Result<bool>.Fail(Messages.RelationExists);
        
        disease.Is_Deleted = true;
        
        int result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        return Result<bool>.Success(true, Messages.DeletedSuccessfully);
    }
}
