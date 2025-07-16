using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Disease.Disease.Commands.Create;

public class CreateDiseaseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
 : BaseHandler<CreateDiseaseCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Disease.Disease> _diseaseRepository = unitOfWork.GetRepository<Domain.Entities.Disease.Disease>();


    public override async Task<Result<string>> Handle(CreateDiseaseCommand request, CancellationToken cancellationToken)
    {
        var disease = mapper.Map<Domain.Entities.Disease.Disease>(request);

        await _diseaseRepository.AddAsync(disease);
        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.SuccessfullyCreated);
    }
}
