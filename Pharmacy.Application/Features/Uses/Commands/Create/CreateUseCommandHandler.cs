using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Uses.Commands.Create;

public class CreateUseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<CreateUseCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Uses.Use> _useRepository = unitOfWork.GetRepository<Domain.Entities.Uses.Use>();

    public override async Task<Result<string>> Handle(CreateUseCommand request, CancellationToken cancellationToken)
    {
        var use = mapper.Map<Domain.Entities.Uses.Use>(request);
        await _useRepository.AddAsync(use);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Success(Messages.SuccessfullyCreated);
    }
}
