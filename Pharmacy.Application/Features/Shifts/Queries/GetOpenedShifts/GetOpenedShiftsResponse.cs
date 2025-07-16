namespace Pharmacy.Application.Features.Shifts.Queries.GetOpenedShifts;
    public record GetOpenedShiftsResponse
    (
        Guid Id,
        string OpenedBy,
        DateTime openedAt
    );

