using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Supplier.Commands.Create;

public class CreateSupplierCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<CreateSupplierCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Supplier.Supplier> _supplierRepository = unitOfWork.GetRepository<Domain.Entities.Supplier.Supplier>();

    public override async Task<Result<string>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = mapper.Map<Domain.Entities.Supplier.Supplier>(request);
        await _supplierRepository.AddAsync(supplier);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Success(Messages.SuccessfullyCreated);
    }
}
