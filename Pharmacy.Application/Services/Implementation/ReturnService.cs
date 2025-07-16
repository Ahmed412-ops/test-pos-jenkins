using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Features.Return.Commands.Create;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Application.Services.Abstraction.ReturnService;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Medicine;
using Pharmacy.Domain.Entities.Wallets;
using Pharmacy.Domain.Entities.Wallets.Return;
using Pharmacy.Domain.Entities.Wallets.Sales;

namespace Pharmacy.Application.Services.Implementation;

public class ReturnService(IUnitOfWork unitOfWork) : IReturnService
{
    private readonly IGenericRepository<PrescriptionItem> _prescriptionItemRepo =
        unitOfWork.GetRepository<PrescriptionItem>();
    private readonly IGenericRepository<MedicineUnit> _medicineUnitRepo =
        unitOfWork.GetRepository<MedicineUnit>();
    private readonly IGenericRepository<ShiftWallet> _shiftWalletRepo =
        unitOfWork.GetRepository<ShiftWallet>();
    private readonly IGenericRepository<Prescription> _prescriptionRepo =
        unitOfWork.GetRepository<Prescription>();

    public async Task<Result<CreateReturnResponse>> ProcessReturnAsync(Return returnRequest)
    {
        var prescription = await _prescriptionRepo.FindAsync(p =>
            p.Id == returnRequest.PrescriptionId
        );

        if (prescription == null)
            return Result<CreateReturnResponse>.Fail(Messages.PrescriptionNotFound);

        var response = new CreateReturnResponse();
        foreach (var item in returnRequest.Items)
        {
            var prescriptionItem = await _prescriptionItemRepo.FindAsync(
                p => p.Id == item.PrescriptionItemId,
                Include: p =>
                    p.Include(pi => pi.MedicationStock)
                        .ThenInclude(pi => pi.Medicine)
                        .Include(pi => pi.MedicineUnit)
                        .ThenInclude(pi => pi.Unit)
                        .Include(pi => pi.Prescription)
            );

            if (prescriptionItem == null)
                return Result<CreateReturnResponse>.Fail(Messages.ItemNotFound);

            if (item.QuantityReturned > prescriptionItem.AvailableForReturn)
                return Result<CreateReturnResponse>.Fail(
                    Messages.QuantityExceedsAvailableForReturn
                );

            if (
                item.QuantityReturned < prescriptionItem.Quantity
                && !await CanReturnPartial(prescriptionItem.MedicineUnitId)
            )
                return Result<CreateReturnResponse>.Fail(Messages.PartialReturnNotAllowed);

            item.AmountRefunded = CalculateItemRefund(prescriptionItem, item);
            prescriptionItem.ReturnedQuantity += item.QuantityReturned;
            prescriptionItem.MedicationStock.Quantity += item.QuantityReturned;
            response.TotalRefunded += item.AmountRefunded;
            response.Items.Add(
                new ReturnItemResponse
                {
                    PrescriptionItemId = prescriptionItem.Id,
                    MedicineName = prescriptionItem.MedicationStock.Medicine.Name,
                    MedicineUnitName = prescriptionItem.MedicineUnit.Unit.Name,
                    QuantityReturned = item.QuantityReturned,
                    AmountRefunded = item.AmountRefunded,
                    IsDamaged = item.IsDamaged,
                    Reason = item.Reason,
                }
            );
        }
        await UpdateFinancialRecords(prescription, returnRequest);

        await unitOfWork.SaveChangesAsync();
        return Result<CreateReturnResponse>.Success(response);
    }

    private async Task<bool> CanReturnPartial(Guid medicineUnitId)
    {
        var unit = await _medicineUnitRepo.FindAsync(m => m.Id == medicineUnitId);
        return unit?.CanBeSold == true && unit.CalcUnit && unit.QuantityForCalcUnit > 1;
    }

    private static decimal CalculateItemRefund(PrescriptionItem item, ReturnItem returnItem)
    {
        var originalPrice = item.UnitPrice * returnItem.QuantityReturned;
        var discountAmount = item.AppliedDiscount * returnItem.QuantityReturned;
        var refundAmount = originalPrice - discountAmount;

        return Math.Round(refundAmount, 2);
    }

    private async Task UpdateFinancialRecords(Prescription prescription, Return returnRequest)
    {
        var shiftWallet =
            await _shiftWalletRepo.FindAsync(
                sw => sw.Id == returnRequest.ShiftWalletId && sw.ShiftId == prescription.ShiftId,
                Include: sw => sw.Include(x => x.ReturnTransactions)
            ) ?? throw new InvalidOperationException(Messages.ShiftWalletDoesNotExist);

        shiftWallet.ReturnTransactions.Add(returnRequest);
    }
}
