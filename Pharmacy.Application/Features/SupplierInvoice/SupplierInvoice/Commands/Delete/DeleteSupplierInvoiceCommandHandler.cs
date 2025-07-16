using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.Delete;

public class DeleteSupplierInvoiceCommandHandler(IUnitOfWork unitOfWork)
    : BaseHandler<DeleteSupplierInvoiceCommand, Result<bool>>
{
    private readonly IGenericRepository<Domain.Entities.SupplierInvoice.SupplierInvoice> _supplierInvoiceRepository =
        unitOfWork.GetRepository<Domain.Entities.SupplierInvoice.SupplierInvoice>();
    
    public override async Task<Result<bool>> Handle(
        DeleteSupplierInvoiceCommand request,
        CancellationToken cancellationToken
    )
    {
        var supplierInvoice = await _supplierInvoiceRepository.FindAsync(
            si => si.Id == request.Id);
        
        if (supplierInvoice == null)
            return Result<bool>.Fail(Messages.NotFound);

        supplierInvoice.Is_Deleted = true;

        int result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        return Result<bool>.Success(true, Messages.DeletedSuccessfully);
    }
}
