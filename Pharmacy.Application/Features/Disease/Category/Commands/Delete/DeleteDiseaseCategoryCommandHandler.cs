using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Disease;

namespace Pharmacy.Application.Features.Disease.Category.Commands.Delete;

public class DeleteDiseaseCategoryCommandHandler(IUnitOfWork unitOfWork)
    : BaseHandler<DeleteDiseaseCategoryCommand, Result<bool>>
{
    private readonly IGenericRepository<DiseaseCategory> _diseaseCategoryRepository = unitOfWork.GetRepository<DiseaseCategory>();
    private readonly IGenericRepository<Domain.Entities.Disease.Disease> _diseaseRepository = unitOfWork.GetRepository<Domain.Entities.Disease.Disease>();

    public override async Task<Result<bool>> Handle(DeleteDiseaseCategoryCommand request, CancellationToken cancellationToken)
    {
        var diseaseCategory = await _diseaseCategoryRepository.FindAsync(d => d.Id == request.Id);
        if (diseaseCategory == null)
            return Result<bool>.Fail(Messages.NotFound);

        bool hasRelations = await _diseaseRepository.IsExistsAsync(d => d.DiseaseCategoryId == request.Id);
        if (hasRelations)
            return Result<bool>.Fail(Messages.RelationExists);

        diseaseCategory.Is_Deleted = true;
        
        int result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        return Result<bool>.Success(true, Messages.DeletedSuccessfully);
    }
}
