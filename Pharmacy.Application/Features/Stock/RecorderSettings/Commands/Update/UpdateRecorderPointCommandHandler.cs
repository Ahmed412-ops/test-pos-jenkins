using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.RecorderSettings.Commands.Update;

public class UpdateRecorderPointCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<UpdateRecorderPointCommand, Result<string>>
    {
        private readonly IGenericRepository<Domain.Entities.Stock.RecorderPointSettings> _recorderPointRepository = unitOfWork.GetRepository<Domain.Entities.Stock.RecorderPointSettings>();
        public override async Task<Result<string>> Handle(UpdateRecorderPointCommand request, CancellationToken cancellationToken)
        {
            var recorderPoint = await _recorderPointRepository.FindAsync(
                s => s.Id == request.Id);
                
            if (recorderPoint == null)
                return Result<string>.Fail(Messages.NotFound);
    
            mapper.Map(request, recorderPoint);
    
            await unitOfWork.SaveChangesAsync();
    
            return Result<string>.Success(Messages.SuccessfullyUpdated);
        }
    }

