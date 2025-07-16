using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.CompleteInvoices;

public class CompleteInvoicesCommandHandler(IUnitOfWork unitOfWork)
    : BaseHandler<CompleteInvoicesCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.SupplierInvoice.SupplierInvoice> _supplierInvoiceRepository = unitOfWork.GetRepository<Domain.Entities.SupplierInvoice.SupplierInvoice>();
    public override async Task<Result<string>> Handle(
        CompleteInvoicesCommand request,
        CancellationToken cancellationToken
    )
    {
        var distinctInvoiceIds = request.InvoiceIds.Distinct().ToList();

        var supplierInvoices = (await _supplierInvoiceRepository
            .GetAllQueryableAsync(s => distinctInvoiceIds.Contains(s.Id)))  
            .Where(s => distinctInvoiceIds.Contains(s.Id))
            .ToList();

        if (supplierInvoices.Count != distinctInvoiceIds.Count)
            return Result<string>.Fail(Messages.SupplierInvoiceNotFound);
            
        foreach (var supplierInvoice in supplierInvoices)
        {
            supplierInvoice.AmountPaid = supplierInvoice.FinalInvoiceTotal;
            supplierInvoice.PaymentStatus = Domain.Enum.PaymentStatus.Paid;
        }
        
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Success(Messages.SuccessfullyCreated);
    }
}
