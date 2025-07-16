using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Uses.Commands.Update;

public class UpdateUseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<UpdateUseCommand, Result<string>>
{
   private readonly IGenericRepository<Domain.Entities.Uses.Use> _useRepository = unitOfWork.GetRepository<Domain.Entities.Uses.Use>();

    public override async Task<Result<string>> Handle(UpdateUseCommand request, CancellationToken cancellationToken)
    {
        var use = await _useRepository.FindAsync(
            s => s.Id == request.Id);

        if (use == null)
            return Result<string>.Fail(Messages.UseNotFound);

        mapper.Map(request, use);

        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.SuccessfullyUpdated);
    }
}
