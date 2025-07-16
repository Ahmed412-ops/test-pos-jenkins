using Pharmacy.Application.Features.Return.Commands.Create;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Wallets.Return;

namespace Pharmacy.Application.Services.Abstraction.ReturnService;

public interface IReturnService
{
    Task<Result<CreateReturnResponse>> ProcessReturnAsync(Return returnRequest);
}
