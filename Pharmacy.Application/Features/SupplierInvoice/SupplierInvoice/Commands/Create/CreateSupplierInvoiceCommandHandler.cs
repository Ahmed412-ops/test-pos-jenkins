using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Application.Services.Abstraction.FileHandler;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.SupplierTransaction;
using Pharmacy.Domain.Enum;
using static Pharmacy.Application.Constants.Permissions.PermissionConstant;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.Create;

public class CreateSupplierInvoiceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileHandler fileHandler)
 : BaseHandler<CreateSupplierInvoiceCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.SupplierInvoice.SupplierInvoice> _supplierInvoiceRepository = unitOfWork.GetRepository<Domain.Entities.SupplierInvoice.SupplierInvoice>();
    public override async Task<Result<string>> Handle(CreateSupplierInvoiceCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsRecevied && request.InvoiceItems.Any())
            return Result<string>.Fail(Messages.InvoiceMustBeReceivedFirst);
        var supplierInvoice = mapper.Map<Domain.Entities.SupplierInvoice.SupplierInvoice>(request);

        supplierInvoice.FinalInvoiceTotal =
         CalculateFinalInvoiceTotal(request.InvoiceItems, request.ShippingFees);
        supplierInvoice.PaymentStatus =
        DeterminePaymentStatus(request.AmountPaid, supplierInvoice.FinalInvoiceTotal);

        supplierInvoice.InvoiceAttachmentUrl = await fileHandler.MoveFile(request.InvoiceAttachmentUrl,
         nameof(FeaturesEnum.SupplierInvoice));
        if (supplierInvoice.PaymentStatus != PaymentStatus.Unpaid)
        {
            supplierInvoice.SupplierTransactions.Add(new SupplierTransaction
            {
                Amount = request.AmountPaid,
                PaymentDate = request.PaymentDate!.Value,
                PaymentMethod = request.PaymentMethod,
                ShiftWalletId = request.ShiftWalletId,
                Notes = request.Notes
            });
        }

        await _supplierInvoiceRepository.AddAsync(supplierInvoice);
        await unitOfWork.SaveChangesAsync();
        if (request.IsRecevied && !request.InvoiceItems.Any())
            return Result<string>.Success(Messages.WaitingForInvoiceDetails);
        return Result<string>.Success(Messages.SuccessfullyCreated);
    }
    private static decimal CalculateFinalInvoiceTotal
    (List<CreateSupplierInvoiceItemDto> items, decimal shippingFees)
    {
        return items.Sum(i =>
        {
            var basePrice = i.PublicSellingPrice * (1 - i.SupplierDiscountPercentage / 100)
                            - i.DistributorFixedMargin
                            - i.PharmacistFixedMargin;
            var taxed = basePrice * (1 + i.TaxAmount / 100);
            return taxed * i.Quantity;
        }) + shippingFees;
    }
    private static PaymentStatus DeterminePaymentStatus
    (decimal amountPaid, decimal finalTotal)
    {
        if (amountPaid <= 0)
            return PaymentStatus.Unpaid;
        if (amountPaid < finalTotal)
            return PaymentStatus.PartiallyPaid;
        return PaymentStatus.Paid;
    }

}




