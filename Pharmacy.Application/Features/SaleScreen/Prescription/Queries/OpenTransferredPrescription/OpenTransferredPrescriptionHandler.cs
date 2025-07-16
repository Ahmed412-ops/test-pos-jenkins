using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SaleScreen.Prescription.Queries.OpenTransferredPrescription;

public class OpenTransferredPrescriptionHandler(IUnitOfWork unitOfWork,
 IMapper mapper,
 ICurrentUser currentUser)
    : BaseHandler<OpenTransferredPrescriptionQuery, Result<OpenTransferredPrescriptionResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Sales.Prescription> _prescriptionRepository
        = unitOfWork.GetRepository<Domain.Entities.Wallets.Sales.Prescription>();
    public override async Task<Result<OpenTransferredPrescriptionResponse>> Handle(
        OpenTransferredPrescriptionQuery request, CancellationToken cancellationToken)
    {
        var user = currentUser.GetUserId();
        var queryable = await _prescriptionRepository.GetAllQueryableAsync(
            x => x.Id == request.PrescriptionId &&
            x.TransferStatus == Domain.Enum.PrescriptionTransferStatus.Transferred
            && x.Shift.OpenedById == user
        ); 
        var prescriptionDto = await queryable
            .ProjectTo<OpenTransferredPrescriptionResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (prescriptionDto is null)
            return Result<OpenTransferredPrescriptionResponse>.Fail(Messages.PrescriptionNotFound);

        return Result<OpenTransferredPrescriptionResponse>.Success(prescriptionDto);
    }
}