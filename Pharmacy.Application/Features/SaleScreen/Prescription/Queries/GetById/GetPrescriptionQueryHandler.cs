using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SaleScreen.Prescription.Queries.GetById;

public class GetPrescriptionQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetPrescriptionQuery, Result<GetPrescriptionResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Sales.Prescription> _prescriptionRepository =
        unitOfWork.GetRepository<Domain.Entities.Wallets.Sales.Prescription>();

    public override async Task<Result<GetPrescriptionResponse>> Handle(
        GetPrescriptionQuery request,
        CancellationToken cancellationToken
    )
    {
        var prescription = await _prescriptionRepository.FindAsync(
            p => p.Id == request.Id,
            Include: q =>
                q.Include(p => p.Customer)
                    .Include(p => p.Items)
                        .ThenInclude(i => i.MedicationStock)
                        .ThenInclude(ms => ms.Medicine)
                    .Include(p => p.Items)
                        .ThenInclude(i => i.MedicineUnit)
                        .ThenInclude(mu => mu.Unit)
                    .Include(p => p.Shift)
                        .ThenInclude(s => s.OpenedBy)
                    .Include(p => p.Transactions)
                    .Include(p=> p.TransferredByUser!)
        );

        if (prescription is null)
            return Result<GetPrescriptionResponse>.Fail(Messages.PrescriptionNotFound);

        return Result<GetPrescriptionResponse>.Success(
            mapper.Map<GetPrescriptionResponse>(prescription)
        );
    }
}
