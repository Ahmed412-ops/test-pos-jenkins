using Pharmacy.Application.Features.Order.Commands.Create;
using Pharmacy.Application.Features.Order.Commands.Update;
using Pharmacy.Application.Features.Order.Queries.DropDown;
using Pharmacy.Application.Features.Order.Queries.GetAll;
using Pharmacy.Application.Features.Order.Queries.GetById;

namespace Pharmacy.Application.Mapping.Order;

public class OrderProfile : MappingProfileBase
{
    public OrderProfile()
    {
        CreateMap<CreateOrderCommand, Domain.Entities.Order.PurchaseOrder>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.Select(x => new Domain.Entities.Order.PurchaseOrderItem
            {
                MedicineUnitId = x.MedicineUnitId,
                Quantity = x.Quantity
            })));
        
        CreateMap<UpdateOrderCommand, Domain.Entities.Order.PurchaseOrder>()
            .IncludeBase<CreateOrderCommand, Domain.Entities.Order.PurchaseOrder>();
        
        CreateMap<Domain.Entities.Order.PurchaseOrder, GetOrdersResponse>();
        CreateMap<Domain.Entities.Order.PurchaseOrder, GetOrderResponse>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.Select(x => new PurchaseOrderItemResponseDto
            {
                MedicineName = x.MedicineUnit.Medicine.Name,
                MedicineUnit = x.MedicineUnit.Unit.Name,
                MedicineUnitId = x.MedicineUnitId,
                Quantity = x.Quantity
            })));
        
        CreateMap<Domain.Entities.Order.PurchaseOrder, OrdersDropDownQueryResponse>()
            .ForMember(dest => dest.PurchaseOrderNumber, opt => opt.MapFrom(src => src.PurchaseOrderNumber));
    }
}
