using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Medicine.Medicine.Commands.Create;

public class CreateMedicineCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
 : BaseHandler<CreateMedicineCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Medicine.Medicine> _medicineRepository = unitOfWork.GetRepository<Domain.Entities.Medicine.Medicine>();


    public override async Task<Result<string>> Handle(CreateMedicineCommand request, CancellationToken cancellationToken)
    {
        var medicine = mapper.Map<Domain.Entities.Medicine.Medicine>(request);

        await _medicineRepository.AddAsync(medicine);
        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.SuccessfullyCreated);
    }
}

