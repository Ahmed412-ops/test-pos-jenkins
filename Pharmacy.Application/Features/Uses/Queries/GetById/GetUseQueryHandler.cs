using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Uses.Queries.GetById;

public class GetUseQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetUseQuery, Result<GetUseResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Uses.Use> _useRepo = unitOfWork.GetRepository<Domain.Entities.Uses.Use>();

    public override async Task<Result<GetUseResponse>> Handle(
        GetUseQuery request,
        CancellationToken cancellationToken
    )
    {
        var use = await _useRepo.FindAsync(m => m.Id == request.Id);

        if (use == null)
            return Result<GetUseResponse>.Fail(Messages.UseNotFound);

        var response = mapper.Map<GetUseResponse>(use);
        return Result<GetUseResponse>.Success(response);
    }
}
