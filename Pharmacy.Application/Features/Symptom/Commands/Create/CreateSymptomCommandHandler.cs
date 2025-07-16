using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Symptom.Commands.Create;

public class CreateSymptomCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<CreateSymptomCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Symptoms.Symptom> _symptomRepository = unitOfWork.GetRepository<Domain.Entities.Symptoms.Symptom>();

    public override async Task<Result<string>> Handle(CreateSymptomCommand request, CancellationToken cancellationToken)
    {
        var symptom = mapper.Map<Domain.Entities.Symptoms.Symptom>(request);
        await _symptomRepository.AddAsync(symptom);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Success(Messages.SuccessfullyCreated);
    }
}
