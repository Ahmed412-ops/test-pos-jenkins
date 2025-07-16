using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Application.Services.Abstraction.ReturnService;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Wallets.Return;
using Pharmacy.Domain.Entities.Wallets.Sales;

namespace Pharmacy.Application.Features.Return.Commands.Create;

public class CreateReturnCommandHandler(
    IUnitOfWork unitOfWork,
    IReturnService returnService
) : BaseHandler<CreateReturnCommand, Result<CreateReturnResponse>>
{
    private readonly IGenericRepository<Prescription> prescriptionRepository =
        unitOfWork.GetRepository<Prescription>();
    private readonly IGenericRepository<PrescriptionItem> prescriptionItemRepository =
        unitOfWork.GetRepository<PrescriptionItem>();

    public override async Task<Result<CreateReturnResponse>> Handle(
        CreateReturnCommand request,
        CancellationToken cancellationToken
    )
    {
        var prescription = await prescriptionRepository.FindAsync(p =>
            p.Id == request.PrescriptionId
        );

        if (prescription == null)
            return Result<CreateReturnResponse>.Fail(Messages.PrescriptionNotFound);

        var prescriptionItemIds = request.Items.Select(i => i.PrescriptionItemId).ToList();
        var existingItems = await prescriptionItemRepository.GetAllAsync(pi =>
            prescriptionItemIds.Contains(pi.Id)
        );

        if (existingItems.Count != request.Items.Count)
            return Result<CreateReturnResponse>.Fail(Messages.ItemNotFound);

        var returnRequest = new Domain.Entities.Wallets.Return.Return
        {
            PrescriptionId = request.PrescriptionId,
            ShiftWalletId = request.ShiftWalletId,
            Notes = request.Notes,
            Items = request
                .Items.Select(item => new ReturnItem
                {
                    PrescriptionItemId = item.PrescriptionItemId,
                    QuantityReturned = item.QuantityReturned,
                    AmountRefunded = 0,
                    IsDamaged = item.IsDamaged,
                    Reason = item.Reason ?? string.Empty,
                })
                .ToList(),
        };

        var result = await returnService.ProcessReturnAsync(returnRequest);

        if (result.Succeeded)
            await unitOfWork.SaveChangesAsync();

        return result;
    }
}
