using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.Update;

public class UpdateSupplierInvoiceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<UpdateSupplierInvoiceCommand, Result<string>>
    {
        private readonly IGenericRepository<Domain.Entities.SupplierInvoice.SupplierInvoice> _supplierInvoiceRepository = unitOfWork.GetRepository<Domain.Entities.SupplierInvoice.SupplierInvoice>();

        public override async Task<Result<string>> Handle(UpdateSupplierInvoiceCommand request, CancellationToken cancellationToken)
        {
            var supplierInvoice = await _supplierInvoiceRepository.FindAsync(
                s => s.Id == request.Id,
                Include: s => s.Include(i => i.Supplier)
                            .Include(i => i.InvoiceItems)
                            .ThenInclude(m => m.MedicineUnit)
                            .ThenInclude(m => m.Medicine)
                            .Include(i => i.InvoiceItems)
                            .ThenInclude(m => m.MedicineUnit)
                            .ThenInclude(m => m.Unit));

            if (supplierInvoice == null)
                return Result<string>.Fail(Messages.NotFound);
    
            mapper.Map(request, supplierInvoice);
            supplierInvoice.IsRecevied = false;
            await unitOfWork.SaveChangesAsync();

            return Result<string>.Success(Messages.SuccessfullyUpdated);
        }
    }
