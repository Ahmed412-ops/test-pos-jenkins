using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Symptom.Commands.Update;

public class UpdateSymptomCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<UpdateSymptomCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Symptoms.Symptom> _symptomRepository = unitOfWork.GetRepository<Domain.Entities.Symptoms.Symptom>();

    public override async Task<Result<string>> Handle(UpdateSymptomCommand request, CancellationToken cancellationToken)
    {
        var symptom = await _symptomRepository.FindAsync(
            s => s.Id == request.Id);

        if (symptom == null)
            return Result<string>.Fail(Messages.NotFound);

        mapper.Map(request, symptom);

        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.SuccessfullyUpdated);
    }
}
