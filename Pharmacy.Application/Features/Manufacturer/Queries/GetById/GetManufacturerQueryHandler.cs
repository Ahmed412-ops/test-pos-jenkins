using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Manufacturer.Queries.GetById;

public class GetManufacturerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetManufacturerQuery, Result<GetManufacturerResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Manufacturers.Manufacturer> _manufacturerRepo = unitOfWork.GetRepository<Domain.Entities.Manufacturers.Manufacturer>();

    public override async Task<Result<GetManufacturerResponse>> Handle(
        GetManufacturerQuery request,
        CancellationToken cancellationToken
    )
    {
        var manufacturer = await _manufacturerRepo.FindAsync(m => m.Id == request.Id);

        if (manufacturer == null)
            return Result<GetManufacturerResponse>.Fail(Messages.ManufacturerNotFound);

        var response = mapper.Map<GetManufacturerResponse>(manufacturer);
        return Result<GetManufacturerResponse>.Success(response);
    }
}

