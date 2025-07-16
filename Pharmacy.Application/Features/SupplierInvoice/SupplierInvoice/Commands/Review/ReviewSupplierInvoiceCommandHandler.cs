using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.StockHistory;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Application.Services.Abstraction.InventoryUpdateService;
using Pharmacy.Application.Services.Abstraction.StockHistoryService;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.Review;

public class ReviewSupplierInvoiceCommandHandler(
    IUnitOfWork unitOfWork,
    IInventoryUpdateService inventoryUpdateService,
    IStockHistoryService stockHistoryService,
    ICurrentUser currentUser
) : BaseHandler<ReviewSupplierInvoiceCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.SupplierInvoice.SupplierInvoice> _supplierInvoiceRepository =
        unitOfWork.GetRepository<Domain.Entities.SupplierInvoice.SupplierInvoice>();

    public override async Task<Result<string>> Handle(
        ReviewSupplierInvoiceCommand request,
        CancellationToken cancellationToken
    )
    {
        var invoice = await _supplierInvoiceRepository.FindAsync(
            s => s.Id == request.Id,
            Include: s =>
                s.Include(i => i.Supplier)
                    .Include(i => i.InvoiceItems)
                    .ThenInclude(mi => mi.MedicineUnit)
                    .ThenInclude(m => m.Medicine)
                    .Include(i => i.InvoiceItems)
                    .ThenInclude(mi => mi.MedicineUnit)
                    .ThenInclude(u => u.Unit)
        );

        if (invoice == null)
            return Result<string>.Fail(Messages.NotFound);

        if (invoice.IsReviewed)
            return Result<string>.Fail(Messages.AlreadyReviewed);

        foreach (var item in request.ReviewedItems)
        {
            var invoiceItem = invoice.InvoiceItems.FirstOrDefault(i => i.Id == item.Id);
            if (invoiceItem == null)
                return Result<string>.Fail(Messages.NotFound);

            invoiceItem.ReviewedQuantity = item.ReviewedQuantity;
        }

        invoice.IsReviewed = true;

        await inventoryUpdateService.UpdateInventoryFromReviewedInvoiceAsync(
            invoice,
            cancellationToken
        );

        var logTasks = new List<Task>();
        foreach (var invoiceItem in invoice.InvoiceItems)
        {
            var overallStockLevel = await stockHistoryService.GetOverallStockLevelAsync(
                (Guid)invoiceItem.MedicineUnit.MedicineId!,
                0
            );

            var logDto = new StockHistoryLogDto
            {
                MedicineId = (Guid)invoiceItem.MedicineUnit.MedicineId,
                TransactionType = StockTransactionType.Added,
                QuantityChange = invoiceItem.ReviewedQuantity ?? invoiceItem.Quantity,
                UpdatedStockLevel = overallStockLevel,
                PerformedById = currentUser.GetUserId(),
                TransactionReference = invoice.InvoiceNumber,
                ReasonForChange = "Supplier invoice review update",
                TransactionDate = DateTime.UtcNow,
            };

            logTasks.Add(stockHistoryService.LogTransactionAsync(logDto));
        }
        await Task.WhenAll(logTasks);

        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.ReviewedSuccessfully);
    }
}
