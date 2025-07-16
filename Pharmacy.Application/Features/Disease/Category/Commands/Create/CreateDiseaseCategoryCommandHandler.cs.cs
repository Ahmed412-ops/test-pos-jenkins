using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Disease.Category.Commands.Create;

public class CreateDiseaseCategorycommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
 : BaseHandler<CreateDiseaseCategoryCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Disease.DiseaseCategory> _diseaseCategoryRepository = unitOfWork.GetRepository<Domain.Entities.Disease.DiseaseCategory>();


    public override async Task<Result<string>> Handle(CreateDiseaseCategoryCommand request, CancellationToken cancellationToken)
    {
        var diseaseCategory = mapper.Map<Domain.Entities.Disease.DiseaseCategory>(request);

        await _diseaseCategoryRepository.AddAsync(diseaseCategory);
        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.SuccessfullyCreated);
    }
}

