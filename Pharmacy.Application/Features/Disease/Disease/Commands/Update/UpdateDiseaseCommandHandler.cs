using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Disease.Disease.Commands.Update;

public class UpdateDiseaseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
 : BaseHandler<UpdateDiseaseCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Disease.Disease> _diseaseRepository = unitOfWork.GetRepository<Domain.Entities.Disease.Disease>();


    public override async Task<Result<string>> Handle(UpdateDiseaseCommand request, CancellationToken cancellationToken)
    {
        var disease = await _diseaseRepository.FindAsync(
            d => d.Id == request.Id,
            Include: d => d.Include(d => d.Symptoms)
                            .ThenInclude(s => s.Symptom!));

        if (disease == null)
            return Result<string>.Fail(Messages.NotFound);

        mapper.Map(request, disease);

        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.SuccessfullyUpdated);
    }
}
