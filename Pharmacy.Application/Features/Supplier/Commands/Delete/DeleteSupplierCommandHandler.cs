using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Supplier.Commands.Delete;

public class DeleteSupplierCommandHandler(IUnitOfWork unitOfWork)
    : BaseHandler<DeleteSupplierCommand, Result<bool>>
{
    private readonly IGenericRepository<Domain.Entities.Supplier.Supplier> _supplierRepo = unitOfWork.GetRepository<Domain.Entities.Supplier.Supplier>();
    public override async Task<Result<bool>> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepo.FindAsync(
            s => s.Id == request.Id,
            Include: s => s.Include(s => s.Contacts));
        
        if (supplier == null)
            return Result<bool>.Fail(Messages.NotFound);
        
        if (supplier.Contacts.Count != 0)
            return Result<bool>.Fail(Messages.RelationExists);
        
        supplier.Is_Deleted = true;
        
        int result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        return Result<bool>.Success(true, Messages.DeletedSuccessfully);
    }
}
