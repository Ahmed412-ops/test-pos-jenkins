using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Shifts.Queries.DropDown
{
    public record CurrentWalletsDropDownQuery : IRequest<Result<List<CurrentWalletsDropDownResponse>>>;
}
