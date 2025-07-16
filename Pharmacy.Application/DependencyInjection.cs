using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Pharmacy.Application.Behaviors;
using Pharmacy.Application.Mapping;
using Pharmacy.Application.Services.Abstraction.AutoPaymentService;
using Pharmacy.Application.Services.Abstraction.CashBackServices;
using Pharmacy.Application.Services.Abstraction.CustomerBalanceService;
using Pharmacy.Application.Services.Abstraction.EmailService;
using Pharmacy.Application.Services.Abstraction.FileHandler;
using Pharmacy.Application.Services.Abstraction.GeneratorService;
using Pharmacy.Application.Services.Abstraction.InventoryUpdateService;
using Pharmacy.Application.Services.Abstraction.MedicineConflictService;
using Pharmacy.Application.Services.Abstraction.PrescriptionTransactionService;
using Pharmacy.Application.Services.Abstraction.ReturnService;
using Pharmacy.Application.Services.Abstraction.SettingService;
using Pharmacy.Application.Services.Abstraction.StockHistoryService;
using Pharmacy.Application.Services.Abstraction.StockManagementService;
using Pharmacy.Application.Services.Implementation;
using Pharmacy.Application.Services.TokenService;

namespace Pharmacy.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register Mediator
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            // Register validation pipeline behavior
            options.AddBehavior(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationPipelineBehavior<,>)
            );
        });

        // Register AutoMapper Profiles
        services.AddAutoMapper(typeof(MappingProfileBase));

        // Register validators
        services.AddValidatorsFromAssembly(
            typeof(DependencyInjection).Assembly,
            includeInternalTypes: true
        );

        services.AddSignalR();
        // Register Services
        services.AddServices();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<ITokenService, TokenService>()
            .AddScoped<IEmailService, EmailService>()
            .AddScoped<IPurchaseOrderNumberGenerator, PurchaseOrderNumberGenerator>()
            .AddScoped<IGenerateLocalBarCode, GenerateLocalBarCode>()
            .AddScoped<IFileHandler, FileHandler>()
            .AddScoped<IInventoryUpdateService, InventoryUpdateService>()
            .AddScoped<IStockHistoryService, StockHistoryService>()
            .AddScoped<IConflictChecker, ConflictChecker>()
            .AddScoped<ICashbackService, CashbackService>()
            .AddScoped<ISettingService, SettingService>()
            .AddScoped<ICustomerWalletService, CustomerWalletService>()
            .AddScoped<ICustomerWalletService, CustomerWalletService>()
            .AddScoped<IStockManagementService, StockManagementService>()
            .AddScoped<IPrescriptionTransactionService, PrescriptionPaymentService>()
            .AddScoped<ICreditValidationService, CreditValidationService>()
            .AddScoped<IAutoPaymentService, AutoPaymentService>()
            .AddScoped<IReturnService, ReturnService>()

        ;
    }
}
