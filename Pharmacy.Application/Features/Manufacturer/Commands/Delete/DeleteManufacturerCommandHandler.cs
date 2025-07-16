using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Manufacturer.Commands.Delete;

public class DeleteManufacturerCommandHandler(IUnitOfWork unitOfWork)
    : BaseHandler<DeleteManufacturerCommand, Result<bool>>
{
    private readonly IGenericRepository<Domain.Entities.Manufacturers.Manufacturer> _manufacturerRepository = unitOfWork.GetRepository<Domain.Entities.Manufacturers.Manufacturer>();
    public override async Task<Result<bool>> Handle(DeleteManufacturerCommand request, CancellationToken cancellationToken)
    {
        var manufacturer = await _manufacturerRepository.FindAsync(
            m => m.Id == request.Id);

        if (manufacturer == null)
            return Result<bool>.Fail(Messages.NotFound);

        manufacturer.Is_Deleted = true;
        
        int result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        return Result<bool>.Success(true, Messages.DeletedSuccessfully);
    }
}
