using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Features.SaleScreen.Prescription.Queries.GetById;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Application.Services.Abstraction.CashBackServices;
using Pharmacy.Application.Services.Abstraction.CustomerBalanceService;
using Pharmacy.Application.Services.Abstraction.PrescriptionTransactionService;
using Pharmacy.Application.Services.Abstraction.SettingService;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Stock;
using Pharmacy.Domain.Entities.Wallets.Sales;

namespace Pharmacy.Application.Features.SaleScreen.Prescription.Commands.Update;

public class UpdateprescriptionCommandHandler(
        IUnitOfWork unitOfWork,
    IMapper mapper,
    IMediator mediator,
    ICreditValidationService creditValidationService,
    ICashbackService cashbackService,
    IPrescriptionTransactionService prescriptionTransactionService,
    ICustomerWalletService walletService
)

 : BaseHandler<UpdatePrescriptionCommand, Result<GetPrescriptionResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Sales.Prescription> _prescriptionRepository =
    unitOfWork.GetRepository<Domain.Entities.Wallets.Sales.Prescription>();
    private readonly IGenericRepository<MedicationStock> _stockRepo =
        unitOfWork.GetRepository<MedicationStock>();
    private readonly IGenericRepository<BalanceTransaction> _balanceRepo =
        unitOfWork.GetRepository<BalanceTransaction>();
    public override async Task<Result<GetPrescriptionResponse>> Handle
    (UpdatePrescriptionCommand request, CancellationToken cancellationToken)
    {
        var prescription = await _prescriptionRepository
            .FindAsync(x => x.Id == request.PrescriptionId,
            Include: x => x
                .Include(x => x.Items)
                    .ThenInclude(x => x.MedicationStock));

        mapper.Map(request, prescription);
        UpdatePrescriptionItemsFromDto(prescription!, request.PrescriptionItems);

        var itemsToReserve = prescription!
                .Items.Select(i => (i.MedicationStockId, i.Quantity))
                .ToList();

        var queryableStocks = await _stockRepo.GetAllQueryableAsync(
            ms => itemsToReserve.Select(x => x.MedicationStockId).Contains(ms.Id),
            asNoTracking: false
        );
        var stocks = await queryableStocks.ToListAsync();

        var stockDict = stocks.ToDictionary(s => s.Id);

        foreach (var item in prescription.Items)
        {
            if (!stockDict.TryGetValue(item.MedicationStockId, out var ms))
                return Result<GetPrescriptionResponse>.Fail(Messages.MedicationStockNotFound);

            item.UnitPrice = ms.SellingPrice;
            ms.Quantity -= item.Quantity;
        }
        var validateCreditResult = await creditValidationService.ValidateAsync(prescription);
        if (!validateCreditResult.Succeeded)
            return Result<GetPrescriptionResponse>.Fail(validateCreditResult.Messages);

        var isEligible = await cashbackService.ValidateCashbackEligibility(prescription);
        if (isEligible.Succeeded && request.CustomerId.HasValue)
        {
            var currentBalance = await GetCashbackBalance(prescription.CustomerId!.Value);
            var actualUsed = Math.Min(request.CashbackUsed, currentBalance);
            prescription.CashbackUsed = actualUsed;

            if (actualUsed > 0)
            {
                await walletService.LogCashbackUsedAsync(
                    prescription.CustomerId.Value,
                    prescription.Id,
                    actualUsed
                );
            }
            else
            {
                var earned = await cashbackService.CalculateEarnedCashback(prescription.Amount);
                await walletService.LogCashbackEarnedAsync(
                    prescription.CustomerId!.Value,
                    prescription.Id,
                    earned
                );
                prescription.CashbackEarned = earned;
            }
        }

        foreach (var payment in request.Payments)
        {
            await prescriptionTransactionService.ApplyTransactionAsync(
                prescription,
                payment.Amount,
                payment.ShiftWalletId
            );
        }

        await unitOfWork.SaveChangesAsync();

        return await mediator.Send(
            new GetPrescriptionQuery { Id = prescription.Id },
            cancellationToken
        );

    }

    private void UpdatePrescriptionItemsFromDto(
        Domain.Entities.Wallets.Sales.Prescription prescription,
        List<UpdatePrescriptionItemDto> newItems)
    {
        var existingItemsDict = prescription.Items.ToDictionary(x => x.Id);

        var updatedItemIds = new HashSet<Guid>();

        foreach (var requestItem in newItems)
        {
            if (requestItem.PrescriptionItemId != Guid.Empty &&
                existingItemsDict.TryGetValue(requestItem.PrescriptionItemId, out var existingItem))
            {
                // Update existing item using AutoMapper
                mapper.Map(requestItem, existingItem);
                updatedItemIds.Add(requestItem.PrescriptionItemId);
            }
            else
            {
                var newItem = mapper.Map<Domain.Entities.Wallets.Sales.PrescriptionItem>(requestItem);
                newItem.Id = Guid.NewGuid();
                newItem.PrescriptionId = prescription.Id;
                prescription.Items.Add(newItem);
            }
        }

        var itemsToRemove = prescription.Items
            .Where(x => !updatedItemIds.Contains(x.Id))
            .Where(x => !newItems.Any(ni => ni.PrescriptionItemId == Guid.Empty)) 
            .ToList();

        foreach (var itemToRemove in itemsToRemove)
        {
            prescription.Items.Remove(itemToRemove);
        }
    }
    private async Task<decimal> GetCashbackBalance(Guid customerId)
    {
        var balanceTransactions = await _balanceRepo.GetAllAsync(t => t.CustomerId == customerId);

        return balanceTransactions
                .Where(t => t.Type == CustomerTransactionType.CashbackEarned)
                .Sum(t => t.Amount)
            - balanceTransactions
                .Where(t => t.Type == CustomerTransactionType.CashbackUsed)
                .Sum(t => t.Amount);
    }
}