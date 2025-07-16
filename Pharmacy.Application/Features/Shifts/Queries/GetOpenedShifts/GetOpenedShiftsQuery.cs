using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Shifts.Queries.GetOpenedShifts;

public class GetOpenedShiftsQuery : IRequest<Result<List<GetOpenedShiftsResponse>>>;
