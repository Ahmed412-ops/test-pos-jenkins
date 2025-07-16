using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Manufacturer.Commands.Create;

public class CreateManufacturerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<CreateManufacturerCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Manufacturers.Manufacturer> _manufacturerRepository = unitOfWork.GetRepository<Domain.Entities.Manufacturers.Manufacturer>();

    public override async Task<Result<string>> Handle(CreateManufacturerCommand request, CancellationToken cancellationToken)
    {
        var manufacturer = mapper.Map<Domain.Entities.Manufacturers.Manufacturer>(request);
        await _manufacturerRepository.AddAsync(manufacturer);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Success(Messages.SuccessfullyCreated);
    }
}
