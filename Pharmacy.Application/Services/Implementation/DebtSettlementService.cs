// using Pharmacy.Application.Common.Interfaces;
// using Pharmacy.Application.Services.Abstraction.CustomerBalanceService;
// using Pharmacy.Application.Services.Abstraction.DebtSettlementService;
// using Pharmacy.Domain.Entities.Wallets.Sales;

// namespace Pharmacy.Application.Services.Implementation;

// public class DebtSettlementService(
//     IUnitOfWork unitOfWork,
//     ICustomerWalletService customerWalletService
// ) : IDebtSettlementService
// {
//     private readonly IGenericRepository<Prescription> _prescriptionRepository =
//         unitOfWork.GetRepository<Prescription>();

//     public async Task<decimal> SettleCustomerDebtsAsync(Guid customerId)
//     {
//         var unpaidPrescriptions = await _prescriptionRepository.GetAllAsync(
//             x => x.CustomerId == customerId && x.AmountDue > 0,
//             orderBy: q => q.OrderBy(x => x.Created_At)
//         );

//         if (unpaidPrescriptions.Count == 0)
//             return 0;

//         // 2. Get available credit
//         var availableCredit = await customerWalletService.GetCreditBalanceAsync(customerId);
//         if (availableCredit <= 0)
//             return 0;

//         foreach (var prescription in unpaidPrescriptions)
//         {
//             if (availableCredit <= 0)
//                 break;

//             var amountToPay = Math.Min(prescription.AmountDue, availableCredit);

//             // 3. Update prescription payment
//             prescription.AmountPaid += amountToPay;
//             prescription.AmountDue -= amountToPay;

//             // 4. Deduct credit
//             await customerWalletService.UseCreditAsync(
//                 customerId,
//                 amountToPay,
//                 $"Auto debt settlement for invoice {prescription.InvoiceNumber}",
//                 prescription.Id
//             );

//             availableCredit -= amountToPay;
//         }

//         await unitOfWork.SaveChangesAsync();
//         return availableCredit;
//     }
// }
