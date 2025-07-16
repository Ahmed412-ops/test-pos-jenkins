using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Manufacturer.Commands.Update;

public class UpdateManufacturerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<UpdateManufacturerCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Manufacturers.Manufacturer> _manufacturerRepository = unitOfWork.GetRepository<Domain.Entities.Manufacturers.Manufacturer>();

    public override async Task<Result<string>> Handle(UpdateManufacturerCommand request, CancellationToken cancellationToken)
    {
        var manufacturer = await _manufacturerRepository.FindAsync(
            s => s.Id == request.Id);

        if (manufacturer == null)
            return Result<string>.Fail(Messages.ManufacturerNotFound);

        mapper.Map(request, manufacturer);

        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.SuccessfullyUpdated);
    }
}
