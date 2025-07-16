using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Supplier.Commands.Update;

public class UpdateSupplierCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
 : BaseHandler<UpdateSupplierCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Supplier.Supplier> _supplierRepository = unitOfWork.GetRepository<Domain.Entities.Supplier.Supplier>();

    public override async Task<Result<string>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.FindAsync(
            s => s.Id == request.Id,
            Include: s => s.Include(s => s.Contacts));

        if (supplier == null)
            return Result<string>.Fail(Messages.NotFound);

        mapper.Map(request, supplier);

        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.SuccessfullyUpdated);
    }
}
