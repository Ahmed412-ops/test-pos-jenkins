using Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.AddTransaction;
using Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Queries.GetTransactions;

namespace Pharmacy.Application.Mapping.SupplierTransaction;

public class TransactionProfile : MappingProfileBase
{
    public TransactionProfile()
    {
        CreateMap<Domain.Entities.SupplierTransaction.SupplierTransaction, GetTransactionsResponse>()
            .ForMember(d => d.InvoiceNumber, s => s.MapFrom(a => a.SupplierInvoice!.InvoiceNumber))
            .ForMember(d => d.RemainingBalance, s => s.MapFrom(a => a.SupplierInvoice!.RemainingBalance))
            .ForMember(d => d.SupplierId, s => s.MapFrom(a => a.SupplierInvoice!.Supplier!.Id))
            .ForMember(d => d.SupplierName, s => s.MapFrom(a => a.SupplierInvoice!.Supplier!.Name));

        CreateMap<AddTransactionCommand, Domain.Entities.SupplierTransaction.SupplierTransaction>();
    }
}
