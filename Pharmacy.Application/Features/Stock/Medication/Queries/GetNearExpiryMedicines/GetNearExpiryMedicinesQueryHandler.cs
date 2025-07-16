using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.Medication.Queries.GetNearExpiryMedicines;

public class GetNearExpiryMedicinesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetNearExpiryMedicinesQuery, Result<PaginationResponse<GetNearExpiryMedicinesResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Stock.MedicationStock> _medicationStockRepo = unitOfWork.GetRepository<Domain.Entities.Stock.MedicationStock>();
    public override async Task<Result<PaginationResponse<GetNearExpiryMedicinesResponse>>> Handle(
        GetNearExpiryMedicinesQuery request,
        CancellationToken cancellationToken
    )
    {
        // Calculate the threshold date based on the requested months
        var thresholdDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(request.MonthsThreshold));

        var (startDate, endDate) = ParseDateFilters(request.ExpiryDateFrom, request.ExpiryDateTo);

        var query = await _medicationStockRepo.GetAllQueryableAsync(
            ms => !ms.Is_Deleted && ms.ExpiryDate <= thresholdDate,
            Include: query => query.Include(ms => ms.Medicine)
        );      
        query = ApplyFilters(query, request.MedicineName, startDate, endDate);

        query = query.OrderBy(ms => ms.ExpiryDate);
        var count = await query.CountAsync(cancellationToken);

        var response = query
            .Select(ms => mapper.Map<GetNearExpiryMedicinesResponse>(ms))
            .Paginate(request)
            .ToList();

        return Result<PaginationResponse<GetNearExpiryMedicinesResponse>>.Success(
            new PaginationResponse<GetNearExpiryMedicinesResponse>
            {
                Data = response,
                Count = count
            },
            response.Any() ? Messages.NearExpiryMedicinesRetrieved : Messages.NoNearExpiryMedicinesFound
        );
    }

    private static (DateOnly? startDate, DateOnly? endDate) ParseDateFilters(DateOnly? expiryDateFrom, DateOnly? expiryDateTo)
    {
        DateOnly? startDate = expiryDateFrom?.WithDay(1);
        DateOnly? endDate = expiryDateTo?.WithDay(DateTime.DaysInMonth(expiryDateTo.Value.Year, expiryDateTo.Value.Month));

        return (startDate, endDate);
    }

    private static IQueryable<Domain.Entities.Stock.MedicationStock> ApplyFilters(
        IQueryable<Domain.Entities.Stock.MedicationStock> query,
        string? medicineName,
        DateOnly? startDate,
        DateOnly? endDate)
    {
        if (!string.IsNullOrWhiteSpace(medicineName))
        {
            query = query.Where(ms => ms.Medicine.Name.Contains(medicineName));
        }

        if (startDate.HasValue)
        {
            query = query.Where(ms => ms.ExpiryDate >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(ms => ms.ExpiryDate <= endDate.Value);
        }

        return query;
    }

}