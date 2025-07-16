using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.DosageForm.Queries.GetById;

public class GetDosageFormQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetDosageFormQuery, Result<GetDosageFormResponse>>
{
    private readonly IGenericRepository<Domain.Entities.DosageForm.DosageForm> _dosageFormRepo = unitOfWork.GetRepository<Domain.Entities.DosageForm.DosageForm>();

    public override async Task<Result<GetDosageFormResponse>> Handle(
        GetDosageFormQuery request,
        CancellationToken cancellationToken
    )
    {
        var dosageForm = await _dosageFormRepo.FindAsync(df => df.Id == request.Id );

        if (dosageForm == null)
            return Result<GetDosageFormResponse>.Fail(Messages.NotFound);

        var response = mapper.Map<GetDosageFormResponse>(dosageForm);
        return Result<GetDosageFormResponse>.Success(response);
    }
}
