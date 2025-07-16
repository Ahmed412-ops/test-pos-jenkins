using Pharmacy.Application.Features.Shifts.CashExpenses.Commands.Create;
using Pharmacy.Application.Features.Shifts.CashExpenses.Commands.Update;
using Pharmacy.Application.Features.Shifts.CashExpenses.Queries.GetAll;
using Pharmacy.Application.Features.Shifts.CashExpenses.Queries.GetById;

namespace Pharmacy.Application.Mapping.Shift.CashExpenses;

class CashExpensesProfile : MappingProfileBase
{
    public CashExpensesProfile()
    {
        CreateMap<CreateCashExpenseCommand, Domain.Entities.Wallets.Expense.CashExpense>()
            .ForMember(dest => dest.ShiftWalletId, opt => opt.Ignore());
        CreateMap<UpdateCashExpenseCommand, Domain.Entities.Wallets.Expense.CashExpense>()
            .IncludeBase<CreateCashExpenseCommand, Domain.Entities.Wallets.Expense.CashExpense>();

        CreateMap<Domain.Entities.Wallets.Expense.CashExpense, GetCashExpenseResponse>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.ShiftWallet, opt => opt.MapFrom(src => src.ShiftWallet.Wallet.Name));

        CreateMap<Domain.Entities.Wallets.Expense.CashExpense, GetCashExpensesResponse>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.Full_Name, opt => opt.MapFrom(src => src.ShiftWallet.Shift.OpenedBy.Full_Name))
            .ForMember(dest => dest.ShiftId, opt => opt.MapFrom(src => src.ShiftWallet.ShiftId))
            .ForMember(dest => dest.IsOpen, opt => opt.MapFrom(src => src.ShiftWallet.Shift.ClosedAt == null));

    }
}
