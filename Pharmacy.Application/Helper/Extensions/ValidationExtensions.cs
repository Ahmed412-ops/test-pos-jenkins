using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;

namespace Pharmacy.Application.Helper.Extensions;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<T, Guid> MustExistDiseaseCategory<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(Messages.CategoryIdRequired)
            .MustAsync(
                async (diseaseCategoryId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.Disease.DiseaseCategory>()
                        .IsExistsAsync(dc => dc.Id == diseaseCategoryId);
                    return exists;
                }
            )
            .WithMessage(Messages.CategoryDoesNotExist);
    }

    public static IRuleBuilderOptions<T, Guid> MustExistEffectiveMaterialCategory<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(Messages.CategoryIdRequired)
            .MustAsync(
                async (CategoryId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.EffectiveMaterial.EffectiveMaterialCategory>()
                        .IsExistsAsync(emc => emc.Id == CategoryId);
                    return exists;
                }
            )
            .WithMessage(Messages.CategoryDoesNotExist);
    }

    public static IRuleBuilderOptions<T, Guid> MustExistSymptom<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .MustAsync(
                async (symptomId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.Symptoms.Symptom>()
                        .IsExistsAsync(s => s.Id == symptomId);
                    return exists;
                }
            )
            .WithMessage(Messages.SymptomDoesNotExist);
    }

    public static IRuleBuilderOptions<T, Guid> MustExistUse<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .MustAsync(
                async (commonUseId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.Uses.Use>()
                        .IsExistsAsync(cu => cu.Id == commonUseId);
                    return exists;
                }
            )
            .WithMessage(Messages.UseDoesNotExist);
    }

    public static IRuleBuilderOptions<T, Guid> MustExistSideEffect<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .MustAsync(
                async (sideEffectId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.SideEffects.SideEffect>()
                        .IsExistsAsync(se => se.Id == sideEffectId);
                    return exists;
                }
            )
            .WithMessage(Messages.SideEffectNotFound);
    }

    public static IRuleBuilderOptions<T, Guid> MustExistFoodInteraction<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .MustAsync(
                async (foodInteractionId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.Food.Food>()
                        .IsExistsAsync(fi => fi.Id == foodInteractionId);
                    return exists;
                }
            )
            .WithMessage(Messages.FoodNotFound);
    }

    public static IRuleBuilderOptions<T, Guid> MustExistDiseaseInteraction<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .MustAsync(
                async (diseaseInteractionId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.Disease.Disease>()
                        .IsExistsAsync(di => di.Id == diseaseInteractionId);
                    return exists;
                }
            )
            .WithMessage(Messages.DiseaseNotFound);
    }

    public static IRuleBuilderOptions<T, Guid> MustExistEffectiveMaterial<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .MustAsync(
                async (effectiveMaterialId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.EffectiveMaterial.EffectiveMaterial>()
                        .IsExistsAsync(em => em.Id == effectiveMaterialId);
                    return exists;
                }
            )
            .WithMessage(Messages.EffectiveMaterialNotFound);
    }

    public static IRuleBuilderOptions<T, Guid> MustExistMedicineCategory<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .MustAsync(
                async (medicineCategoryId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.Medicine.MedicineCategory>()
                        .IsExistsAsync(mc => mc.Id == medicineCategoryId);
                    return exists;
                }
            )
            .WithMessage(Messages.MedicineCategoryNotFound);
    }

    public static IRuleBuilderOptions<T, Guid> MustExistDosageForm<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .MustAsync(
                async (dosageFormId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.DosageForm.DosageForm>()
                        .IsExistsAsync(df => df.Id == dosageFormId);
                    return exists;
                }
            )
            .WithMessage(Messages.DosageFormNotFound);
    }

    public static IRuleBuilderOptions<T, Guid> MustExistManufacturer<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .MustAsync(
                async (manufacturerId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.Manufacturers.Manufacturer>()
                        .IsExistsAsync(m => m.Id == manufacturerId);
                    return exists;
                }
            )
            .WithMessage(Messages.ManufacturerNotFound);
    }

    public static IRuleBuilderOptions<T, Guid> MustExistSellingUnit<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .MustAsync(
                async (sellingUnitId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.Unit.Unit>()
                        .IsExistsAsync(su => su.Id == sellingUnitId);
                    return exists;
                }
            )
            .WithMessage(Messages.UnitNotFound);
    }

    public static IRuleBuilderOptions<T, Guid> MustExistMedicine<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .MustAsync(
                async (medicineId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.Medicine.Medicine>()
                        .IsExistsAsync(m => m.Id == medicineId);
                    return exists;
                }
            )
            .WithMessage(Messages.MedicineNotFound);
    }

    public static IRuleBuilderOptions<T, Guid> MustExistDisease<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .MustAsync(
                async (diseaseId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.Disease.Disease>()
                        .IsExistsAsync(d => d.Id == diseaseId);
                    return exists;
                }
            )
            .WithMessage(Messages.DiseaseNotFound);
    }

    public static IRuleBuilderOptions<T, Guid> MustExistSupplier<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .MustAsync(
                async (supplierId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.Supplier.Supplier>()
                        .IsExistsAsync(s => s.Id == supplierId);
                    return exists;
                }
            )
            .WithMessage(Messages.SupplierIsRequired);
    }

    public static IRuleBuilderOptions<T, Guid> MustExistMedicineUnit<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .MustAsync(
                async (medicineUnitId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.Medicine.MedicineUnit>()
                        .IsExistsAsync(mu => mu.Id == medicineUnitId);
                    return exists;
                }
            )
            .WithMessage(Messages.MedicineUnitNotFound);
    }

    public static IRuleBuilderOptions<T, string> MustBeUniqueSupplierInvoiceNumber<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .MustAsync(
                async (invoiceNumber, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.SupplierInvoice.SupplierInvoice>()
                        .IsExistsAsync(s => s.InvoiceNumber == invoiceNumber);
                    return !exists;
                }
            )
            .WithMessage(Messages.InvoiceNumberAlreadyExists);
    }

    public static IRuleBuilderOptions<T, Guid> MustExistSupplierInvoice<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .MustAsync(
                async (supplierInvoiceId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.SupplierInvoice.SupplierInvoice>()
                        .IsExistsAsync(s => s.Id == supplierInvoiceId);
                    return exists;
                }
            )
            .WithMessage(Messages.SupplierInvoiceNotFound);
    }

    public static IRuleBuilderOptions<T, Guid> MustExistWallet<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .MustAsync(
                async (walletId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.Wallets.Wallet>()
                        .IsExistsAsync(w => w.Id == walletId);
                    return exists;
                }
            )
            .WithMessage(Messages.WalletNotFound);
    }

    public static IRuleBuilderOptions<T, Guid> MustExistShift<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .MustAsync(
                async (shiftId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.Wallets.Shift>()
                        .IsExistsAsync(s => s.Id == shiftId && s.ClosedAt == null);
                    return exists;
                }
            )
            .WithMessage(Messages.ShiftNotFound);
    }
    public static IRuleBuilderOptions<T, Guid> MustExistShiftWallet<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .MustAsync(
                async (shiftWalletId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.Wallets.ShiftWallet>()
                        .IsExistsAsync(s => s.Id == shiftWalletId);
                    return exists;
                }
            )
            .WithMessage(Messages.WalletNotFound);
    }

    public static IRuleBuilderOptions<T, Guid> MustNotInOpenShift<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .MustAsync(
                async (walletId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.Wallets.ShiftWallet>()
                        .IsExistsAsync(s => s.WalletId == walletId && s.Shift.ClosedAt == null);
                    return !exists;
                }
            )
            .WithMessage(Messages.WalletAlreadyInOpenShift);
    }
    public static IRuleBuilderOptions<T, Guid?> MustExistCustomer<T>(
        this IRuleBuilder<T, Guid?> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .MustAsync(
                async (customerId, cancellation) =>
                {
                    if (!customerId.HasValue) return false;
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.Customers.Customer>()
                        .IsExistsAsync(c => c.Id == customerId.Value);
                    return exists;
                }
            )
            .WithMessage(Messages.CustomerNotFound);
    }
    public static IRuleBuilderOptions<T, Guid> MustExistMedicationStock<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .MustAsync(
                async (medicationStockId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.Stock.MedicationStock>()
                        .IsExistsAsync(ms => ms.Id == medicationStockId);
                    return exists;
                }
            )
            .WithMessage(Messages.MedicationStockNotFound);
    }

    public static IRuleBuilderOptions<T, Guid?> MustExistTargetUser<T>(
    this IRuleBuilder<T, Guid?> ruleBuilder,
    IUnitOfWork unitOfWork
)
    {
        return ruleBuilder
            .MustAsync(
                async (TargetUserId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.Stock.MedicationStock>()
                        .IsExistsAsync(ms => ms.Id == TargetUserId);
                    return exists;
                }
            )
            .WithMessage(Messages.TargetUserNotFound);
    }

    public static IRuleBuilderOptions<T, Guid> MustExistPrescription<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        IUnitOfWork unitOfWork
)
    {
        return ruleBuilder
            .MustAsync(
                async (PrescriptionId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.Wallets.Sales.Prescription>()
                        .IsExistsAsync(ms => ms.Id == PrescriptionId);
                    return exists;
                }
            )
            .WithMessage(Messages.PrescriptionNotFound);
    }
    public static IRuleBuilderOptions<T, Guid> MustExistPrescriptionItem<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        IUnitOfWork unitOfWork
    )
    {
        return ruleBuilder
            .MustAsync(
                async (prescriptionItemId, cancellation) =>
                {
                    bool exists = await unitOfWork
                        .GetRepository<Domain.Entities.Wallets.Sales.PrescriptionItem>()
                        .IsExistsAsync(pi => pi.Id == prescriptionItemId);
                    return exists;
                }
            )
            .WithMessage(Messages.PrescriptionItemNotFound);
    }
}
