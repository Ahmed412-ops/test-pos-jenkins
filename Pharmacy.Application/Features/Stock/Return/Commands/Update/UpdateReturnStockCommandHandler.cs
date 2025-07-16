using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.Return.Commands.Update;

public class UpdateReturnStockCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<UpdateReturnStockCommand, Result<string>>
    {
        private readonly IGenericRepository<Domain.Entities.Stock.MedicationReturn> _returnRepository = unitOfWork.GetRepository<Domain.Entities.Stock.MedicationReturn>();
        public override async Task<Result<string>> Handle(UpdateReturnStockCommand request, CancellationToken cancellationToken)
        {
            var stockReturn = await _returnRepository.FindAsync(
                s => s.Id == request.Id,
                Include: s => s.Include(i => i.ReturnItems)
                            .ThenInclude(m => m.MedicineUnit)
                            .Include(i => i.Supplier)
                            .Include(i => i.SupplierInvoice!));
                            
            if (stockReturn == null)
                return Result<string>.Fail(Messages.NotFound);
    
            mapper.Map(request, stockReturn);
    
            await unitOfWork.SaveChangesAsync();
    
            return Result<string>.Success(Messages.SuccessfullyUpdated);
        }
    }
