using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Notification;
using Pharmacy.Application.Features.SaleScreen.Prescription.Queries.GetById;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Application.Services.Abstraction.CashBackServices;
using Pharmacy.Application.Services.Abstraction.CustomerBalanceService;
using Pharmacy.Application.Services.Abstraction.NotificationServices;
using Pharmacy.Application.Services.Abstraction.PrescriptionTransactionService;
using Pharmacy.Application.Services.Abstraction.SettingService;
using Pharmacy.Application.Services.Implementation.RealTimeNotification;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Stock;
using Pharmacy.Domain.Entities.Wallets;
using Pharmacy.Domain.Entities.Wallets.Sales;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.SaleScreen.Prescription.Commands.Create;

public class CreatePrescriptionCommandHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IMediator mediator,
    ICurrentUser currentUser,
    IHubContext<NotificationHub, IRealTimeNotification>
     hubContext,
    ICreditValidationService creditValidationService,
    ICashbackService cashbackService,
    IPrescriptionTransactionService prescriptionTransactionService,
    ICustomerWalletService walletService
) : BaseHandler<CreatePrescriptionCommand, Result<GetPrescriptionResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Sales.Prescription> _prescriptionRepository =
        unitOfWork.GetRepository<Domain.Entities.Wallets.Sales.Prescription>();
    private readonly IGenericRepository<MedicationStock> _stockRepo =
        unitOfWork.GetRepository<MedicationStock>();
    private readonly IGenericRepository<BalanceTransaction> _balanceRepo =
        unitOfWork.GetRepository<BalanceTransaction>();
    private readonly IGenericRepository<Shift> _shiftRepo =
        unitOfWork.GetRepository<Shift>();
    public override async Task<Result<GetPrescriptionResponse>> Handle(
        CreatePrescriptionCommand request,
        CancellationToken cancellationToken
    )
    {
        var prescription = mapper.Map<Domain.Entities.Wallets.Sales.Prescription>(request);

        if (request.IsTransferred == true)
            await HandlePrescriptionTransferAsync(request, prescription);
        var itemsToReserve = prescription
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
        await _prescriptionRepository.AddAsync(prescription);

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

    private async Task<Result<GetPrescriptionResponse>> HandlePrescriptionTransferAsync(
        CreatePrescriptionCommand request,
        Domain.Entities.Wallets.Sales.Prescription prescription)
    {
        var openShift = await _shiftRepo.FindAsync(
            s => s.Id == request.ShiftId && s.ClosedAt == null
        );

        prescription.TransferStatus = PrescriptionTransferStatus.Transferred;
        prescription.TransferredAt = DateTime.UtcNow;
        prescription.TransferredByUserId = currentUser.GetUserId();
        prescription.ShiftId = openShift!.Id;
        prescription.Shift = openShift; 

        var notification = mapper.Map<PushPrescriptionTransferNotification>(prescription);
        await hubContext.Clients
            .User(openShift.OpenedById.ToString())
            .SendNotification(notification);

        return Result<GetPrescriptionResponse>.Success();
    }
}
