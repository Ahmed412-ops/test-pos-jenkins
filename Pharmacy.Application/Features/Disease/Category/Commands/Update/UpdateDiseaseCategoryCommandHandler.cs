using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Disease.Category.Commands.Update;

public class UpdateDiseaseCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
 : BaseHandler<UpdateDiseaseCategoryCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Disease.DiseaseCategory> _diseaseCategoryRepository = unitOfWork.GetRepository<Domain.Entities.Disease.DiseaseCategory>();


    public override async Task<Result<string>> Handle(UpdateDiseaseCategoryCommand request, CancellationToken cancellationToken)
    {
        var diseaseCategory = await _diseaseCategoryRepository.FindAsync(
            d => d.Id == request.Id);

        if (diseaseCategory == null)
            return Result<string>.Fail(Messages.NotFound);

        mapper.Map(request, diseaseCategory);

        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.SuccessfullyUpdated);
    }
}
