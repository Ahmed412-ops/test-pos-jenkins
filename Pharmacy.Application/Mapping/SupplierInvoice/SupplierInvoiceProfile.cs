using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.AddTransaction;
using Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.Create;
using Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.Update;
using Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Queries.GetAll;
using Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Queries.GetById;
using Pharmacy.Domain.Entities.SupplierInvoice;

namespace Pharmacy.Application.Mapping.SupplierInvoice
{
    public class SupplierInvoiceProfile : MappingProfileBase
    {
        public SupplierInvoiceProfile()
        {
            CreateMap<CreateSupplierInvoiceCommand, Domain.Entities.SupplierInvoice.SupplierInvoice>()
                .ForMember(dest => dest.InvoiceItems, opt => opt.MapFrom(src => src.InvoiceItems));

            CreateMap<CreateSupplierInvoiceItemDto, SupplierInvoiceItem>()
                .ForMember(dest => dest.SupplierPurchasePrice, 
                    opt => opt.MapFrom(src => CalculateUnitPrice(src)))
                .ForMember(dest => dest.TotalPurchasePrice, 
                    opt => opt.MapFrom(src => CalculateTotalPrice(src)))
                .ForMember(dest => dest.TaxAmount, 
                    opt => opt.MapFrom(src => CalculateTax(src)))
                .ForMember(dest => dest.FinalPrice, 
                    opt => opt.MapFrom(src => CalculateFinalPrice(src)));

            CreateMap<AddTransactionCommand, Domain.Entities.SupplierTransaction.SupplierTransaction>();

            CreateMap<UpdateSupplierInvoiceCommand, Domain.Entities.SupplierInvoice.SupplierInvoice>()
                .IncludeBase<CreateSupplierInvoiceCommand, Domain.Entities.SupplierInvoice.SupplierInvoice>();        
            
            CreateMap<Domain.Entities.SupplierInvoice.SupplierInvoice, GetSupplierInvoicesResponse>()
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name));

            // Map from Domain Entity to Response DTO
            CreateMap<Domain.Entities.SupplierInvoice.SupplierInvoice, GetSupplierInvoiceResponse>()
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name))
                .ForMember(dest => dest.PurchaseOrderNumber, opt => opt.MapFrom(src => src.PurchaseOrder!.PurchaseOrderNumber))
                .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src =>
                    src.InvoiceItems.Sum(i => ComputeItemTotal(i))))
                .ForMember(dest => dest.TotalDiscount, opt => opt.MapFrom(src =>
                    src.InvoiceItems.Sum(i => ComputeItemDiscount(i))))
                .ForMember(dest => dest.TotalTaxAmount, opt => opt.MapFrom(src =>
                    src.InvoiceItems.Sum(i => ComputeItemTax(i))))
                .ForMember(dest => dest.FinalInvoiceTotal, opt => opt.MapFrom(src =>
                    src.InvoiceItems.Sum(i => ComputeItemTotal(i)) -
                    src.InvoiceItems.Sum(i => ComputeItemDiscount(i)) +
                    src.InvoiceItems.Sum(i => ComputeItemTax(i)) +
                    src.ShippingFees))
                .ForMember(dest => dest.RemainingBalance, opt => opt.MapFrom(src =>
                    src.InvoiceItems.Sum(i => ComputeItemTotal(i)) -
                    src.InvoiceItems.Sum(i => ComputeItemDiscount(i)) +
                    src.InvoiceItems.Sum(i => ComputeItemTax(i)) +
                    src.ShippingFees - src.AmountPaid))
                .ForMember(dest => dest.InvoiceItems, opt => opt.MapFrom(src => src.InvoiceItems));

            CreateMap<SupplierInvoiceItem, SupplierInvoiceItemResponseDto>()
                .ForMember(dest => dest.MedicineName, opt => opt.MapFrom(src => src.MedicineUnit.Medicine.Name))
                .ForMember(dest => dest.MedicineUnit, opt => opt.MapFrom(src => src.MedicineUnit.Unit.Name))
                ;
            
            CreateMap<Domain.Entities.SupplierInvoice.SupplierInvoice, DropDownQueryResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.InvoiceNumber));
        }

        // Helper methods for Create mapping (input DTO to entity)
        private static decimal CalculateUnitPrice(CreateSupplierInvoiceItemDto src) =>
            src.PublicSellingPrice * (1 - src.SupplierDiscountPercentage);

        private static decimal CalculateTotalPrice(CreateSupplierInvoiceItemDto src) =>
            CalculateUnitPrice(src) * src.Quantity;

        //  TaxAmount is the fixed tax per unit, so multiply by quantity
        private static decimal CalculateTax(CreateSupplierInvoiceItemDto src) =>
            src.TaxAmount * src.Quantity;

        private static decimal CalculateFinalPrice(CreateSupplierInvoiceItemDto src) =>
            CalculateTotalPrice(src) + CalculateTax(src);

        // Helper methods for Get mapping (entity to response)
        private static decimal ComputeItemTotal(SupplierInvoiceItem item) =>
            item.Quantity * item.PublicSellingPrice; // full price

        private static decimal ComputeItemDiscount(SupplierInvoiceItem item) =>
            item.Quantity * item.PublicSellingPrice * item.SupplierDiscountPercentage;

        // Updated to directly use the absolute tax amount stored on the item
        private static decimal ComputeItemTax(SupplierInvoiceItem item) =>
            item.TaxAmount;
    }
}
