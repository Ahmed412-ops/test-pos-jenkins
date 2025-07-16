using MediatR;
using Pharmacy.Application.Features.Shifts.CashExpenses.Commands.Comman.Interfaces;
using Pharmacy.Application.Features.Shifts.CashExpenses.Commands.Comman.Validator;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Shifts.CashExpenses.Commands.Create
{
    public class CreateCashExpenseCommand: IRequest<Result<string>> , ICashExpenseCommand
    {
        public Guid CategoryId { get; set; }
        public Guid WalletId { get; set; }
        public decimal Amount { get; set; }
        public string? PaidTo { get; set; }
        public string? Notes { get; set; }
    }
}
