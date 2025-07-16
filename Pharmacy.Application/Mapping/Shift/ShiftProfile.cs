using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Shifts.Queries.DropDown;
using Pharmacy.Application.Features.Shifts.Queries.GetAll;
using Pharmacy.Application.Features.Shifts.Queries.GetById;
using Pharmacy.Application.Features.Shifts.Queries.GetOpenedShifts;

namespace Pharmacy.Application.Mapping.Shift;

public class ShiftProfile : MappingProfileBase
{
    public ShiftProfile()
    {
        CreateMap<Features.Shifts.Commands.Create.OpenShiftCommand, Domain.Entities.Wallets.Shift>()
            .ForMember(dest => dest.ShiftWallets, opt => opt.MapFrom(src => src.Wallets));

        CreateMap<
            Features.Shifts.Commands.Create.OpenShiftWalletDto,
            Domain.Entities.Wallets.ShiftWallet
        >();

        CreateMap<Domain.Entities.Wallets.Shift, GetShiftsResponse>()
            .ForMember(dest => dest.OpenedBy, opt => opt.MapFrom(src => src.OpenedBy.Full_Name));

        CreateMap<Domain.Entities.Wallets.Shift, GetShiftResponse>()
            .ForMember(dest => dest.OpenedBy, opt => opt.MapFrom(src => src.OpenedBy.Full_Name))
            .ForMember(
                dest => dest.ClosedBy,
                opt => opt.MapFrom(src => src.ClosedBy != null ? src.ClosedBy.Full_Name : null)
            )
            .ForMember(
                dest => dest.IncomingTransactions,
                opt =>
                    opt.MapFrom(src => src.ShiftWallets.SelectMany(x => x.PrescriptionTransactions))
            )
            .ForMember(
                dest => dest.OutgoingTransactions,
                opt => opt.MapFrom(src => src.ShiftWallets.SelectMany(x => x.CashExpenses))
            )
            .ForMember(dest => dest.WalletRegisters, opt => opt.MapFrom(src => src.ShiftWallets));
        CreateMap<Domain.Entities.Wallets.ShiftWallet, WalletRegisterdDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Wallet.Name))
            .ForMember(dest => dest.WalletId, opt => opt.MapFrom(src => src.Wallet.Id));
        CreateMap<Domain.Entities.Wallets.Sales.Prescription, ShiftTransactionDto>();
        CreateMap<Domain.Entities.Wallets.Sales.PrescriptionTransaction, ShiftTransactionDto>();
        CreateMap<Domain.Entities.Wallets.Expense.CashExpense, ShiftTransactionDto>();
        CreateMap<Domain.Entities.Wallets.ShiftWallet, CurrentWalletsDropDownResponse>()
            .ForMember(dest => dest.WalletId, opt => opt.MapFrom(src => src.Wallet.Id))
            .ForMember(dest => dest.ShiftWalletId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ShiftId, opt => opt.MapFrom(src => src.Shift.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Wallet.Name));
        CreateMap<Domain.Entities.Wallets.Shift, GetOpenedShiftsResponse>()
            .ForMember(dest => dest.OpenedBy, opt => opt.MapFrom(src => src.OpenedBy.Full_Name)); 
    }
}
