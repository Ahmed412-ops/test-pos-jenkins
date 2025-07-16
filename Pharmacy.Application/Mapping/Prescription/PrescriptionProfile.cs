using Pharmacy.Application.Features.Customer.Queries.GetById.GetByIdResponses;
using Pharmacy.Application.Features.SaleScreen.Prescription.Commands.Create;
using Pharmacy.Application.Features.SaleScreen.Prescription.Commands.Update;
using Pharmacy.Application.Features.SaleScreen.Prescription.Queries.GetAll;
using Pharmacy.Application.Features.SaleScreen.Prescription.Queries.GetById;
using Pharmacy.Application.Features.SaleScreen.Prescription.Queries.OpenTransferredPrescription;
using Pharmacy.Application.Features.SaleScreen.Prescription.Queries.OpenTransferredPrescription.Responses;
using Pharmacy.Domain.Entities.Wallets;
using Pharmacy.Domain.Entities.Wallets.Sales;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Mapping.Prescription;

public class PrescriptionProfile : MappingProfileBase
{
    public PrescriptionProfile()
    {
        CreateMap<CreatePrescriptionCommand, Domain.Entities.Wallets.Sales.Prescription>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.PrescriptionItems))
            .ForMember(dest => dest.Transactions, opt => opt.Ignore());

        CreateMap<UpdatePrescriptionCommand, Domain.Entities.Wallets.Sales.Prescription>()
            .ForMember(dest => dest.Items, opt => opt.Ignore())
            .ForMember(dest => dest.Transactions, opt => opt.Ignore());

        CreateMap<UpdatePrescriptionItemDto, PrescriptionItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PrescriptionId, opt => opt.Ignore());

        CreateMap<CreatePrescriptionItemDto, PrescriptionItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PrescriptionId, opt => opt.Ignore());

        CreateMap<Domain.Entities.Wallets.Sales.Prescription, GetPrescriptionsResponse>()
            .ForMember(
                dest => dest.CustomerName,
                opt => opt.MapFrom(src => src.Customer == null ? "" : src.Customer.Name)
            );

        CreateMap<Domain.Entities.Wallets.Sales.Prescription, GetPrescriptionResponse>()
            .ForMember(
                dest => dest.CustomerName,
                opt => opt.MapFrom(src => src.Customer == null ? "" : src.Customer.Name)
            )
            .ForMember(dest => dest.PrescriptionItems, opt => opt.MapFrom(src => src.Items))
            .ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions))
            .ForMember(
                dest => dest.TotalItems,
                opt => opt.MapFrom(src => src.Items.Sum(i => i.Quantity))
            )
            .ForMember(
                dest => dest.CreatedBy,
                opt => opt.MapFrom(src => src.Shift.OpenedBy.Full_Name)
            )
            .ForMember(
                dest => dest.TransferredBy,
                opt => opt.MapFrom(src => src.TransferStatus == PrescriptionTransferStatus.Transferred
                    ? src.TransferredByUser != null ? src.TransferredByUser.Full_Name : null
                    : null)
            )
            ;

        CreateMap<PrescriptionItem, GetPrescriptionItemResponse>()
            .ForMember(
                dest => dest.MedicineName,
                opt => opt.MapFrom(src => src.MedicationStock.Medicine.Name)
            )
            .ForMember(
                dest => dest.MedicineUnitName,
                opt => opt.MapFrom(src => src.MedicineUnit.Unit.Name)
            )
            .ForMember(
                dest => dest.UnitPrice,
                opt => opt.MapFrom(src => src.MedicationStock.SellingPrice)
            );

        CreateMap<PrescriptionTransaction, PrescriptionTransactionDto>();

        CreateMap<Domain.Entities.Wallets.Sales.Prescription, OpenTransferredPrescriptionResponse>()
            .ForMember(
                dest => dest.TotalItems,
                opt => opt.MapFrom(src => src.Items.Sum(i => i.Quantity)))
            .ForMember(
                    dest => dest.ShiftWallet,
                    opt => opt.MapFrom(src => src.Shift.ShiftWallets.FirstOrDefault()));

        CreateMap<Domain.Entities.Customers.Customer, GetCustomerByIdResponse>()
        .ForMember(
                dest => dest.Age,
                opt =>
                    opt.MapFrom(src =>
                        src.DateOfBirth.HasValue
                            ? (int?)((DateTime.Today - src.DateOfBirth.Value).TotalDays / 365.25)
                            : null
                    ));
        CreateMap<PrescriptionItem, GetPrescriptionItemResponse>()
        .ForMember(
            dest => dest.MedicineName,
            opt => opt.MapFrom(src => src.MedicationStock.Medicine.Name)
        )
        .ForMember(
            dest => dest.MedicineUnitName,
            opt => opt.MapFrom(src => src.MedicineUnit.Unit.Name)
        );
        CreateMap<ShiftWallet, ShiftWalletResponse>()
        .ForMember(dest => dest.WalletName, opt =>
        opt.MapFrom(src => src.Wallet.Name));
        


    }
}
