using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Symptom.Commands.Delete;

public class DeleteSymptomCommandHandler(IUnitOfWork unitOfWork)
 : BaseHandler<DeleteSymptomCommand, Result<bool>>
{
    private readonly IGenericRepository<Domain.Entities.Symptoms.Symptom> _symptomRepository = unitOfWork.GetRepository<Domain.Entities.Symptoms.Symptom>();
    public override async Task<Result<bool>> Handle(DeleteSymptomCommand request, CancellationToken cancellationToken)
    {
        var symptom = await _symptomRepository.FindAsync(
            f => f.Id == request.Id,
            Include: f => f.Include(f => f.Diseases));

        if (symptom == null)
            return Result<bool>.Fail(Messages.NotFound);
        
        if (symptom.Diseases.Count != 0)
            return Result<bool>.Fail(Messages.RelationExists);

        symptom.Is_Deleted = true;
        
        int result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        return Result<bool>.Success(true, Messages.DeletedSuccessfully);
    }
}
