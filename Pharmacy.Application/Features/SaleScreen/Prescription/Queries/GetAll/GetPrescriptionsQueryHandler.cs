using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SaleScreen.Prescription.Queries.GetAll;

public class GetPrescriptionsQueryHandler(IUnitOfWork unitOfWork)
    : BaseHandler<GetPrescriptionsQuery, Result<PaginationResponse<GetPrescriptionsResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Sales.Prescription> _prescriptionRepository =
        unitOfWork.GetRepository<Domain.Entities.Wallets.Sales.Prescription>();

    public override async Task<Result<PaginationResponse<GetPrescriptionsResponse>>> Handle(
        GetPrescriptionsQuery request,
        CancellationToken cancellationToken
    )
    {
        var baseQuery = await _prescriptionRepository.GetAllQueryableAsync(
            d => !d.Is_Deleted,
            Include: i =>
                i.Include(p => p.Customer)
                    .Include(p => p.Items)
                    .Include(p => p.Shift)
                    .ThenInclude(s => s.OpenedBy)
                    .Include(p => p.Transactions),
            orderBy: p => p.OrderBy(x => x.Created_At)
        );

        if (request.InvoiceNumber.HasValue)
            baseQuery = baseQuery.Where(p =>
                p.InvoiceNumber.ToString().Contains(request.InvoiceNumber.Value.ToString())
            );

        if (!string.IsNullOrWhiteSpace(request.CustomerNameOrPhone))
            baseQuery = baseQuery.Where(p =>
                p.Customer != null
                && (
                    p.Customer.Name.Contains(request.CustomerNameOrPhone)
                    || p.Customer.PhoneNumbers.Any(n =>
                        n.Number.Contains(request.CustomerNameOrPhone)
                    )
                )
            );

        if (request.PrescriptionStartDate.HasValue)
            baseQuery = baseQuery.Where(p => p.Created_At >= request.PrescriptionStartDate);

        if (request.PrescriptionEndDate.HasValue)
            baseQuery = baseQuery.Where(p => p.Created_At <= request.PrescriptionEndDate);

        var count = await baseQuery.CountAsync(cancellationToken);

        var page = await baseQuery
            .Select(p => new GetPrescriptionsResponse
            {
                Id = p.Id,
                InvoiceNumber = p.InvoiceNumber,
                CustomerName = p.Customer != null ? p.Customer.Name : null,
                PrescriptionDate = p.Created_At,
                Amount = p.Amount,
                Discount = p.Discount,
                AmountPaid = p.AmountPaid,
                AmountDue = p.AmountDue,
                CashbackEarned = p.CashbackEarned,
                CashbackUsed = p.CashbackUsed,
            })
            .AsSplitQuery()
            .Paginate(request)
            .ToListAsync(cancellationToken);

        return Result<PaginationResponse<GetPrescriptionsResponse>>.Success(
            new PaginationResponse<GetPrescriptionsResponse> { Data = page, Count = count }
        );
    }
}
