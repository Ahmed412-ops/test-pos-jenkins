# Pharmacy



Based on the directory structure, I'll categorize the entities by their priority and business importance:


Core Business Entities (Highest Priority)

Medicine/ - Core product entity
EffectiveMaterial/ - Active ingredients and medical information
Stock/ - Inventory management
Order/ - Sales and transactions


Medical Reference Entities (High Priority)
Disease/ - Medical conditions
SideEffects/ - Medication side effects
Symptoms/ - Disease symptoms
Uses/ - Medication uses
Food/ - Food interactions


Product Management Entities (High Priority)
Manufacturers/ - Medicine manufacturers
DosageForm/ - Medicine forms (tablets, capsules, etc.)
Unit/ - Measurement units
Supplier/ - Medicine suppliers
SupplierInvoice/ - Purchase invoices
SupplierTransaction/ - Supplier transactions


Security and Authentication Entities (Support Priority)
Auth/ - Authentication related
Identity/ - User identity management
Permissions/ - Access control
Base Classes
BaseEntity.cs - Base entity with common properties
EntityModel.cs - Extended base entity model





Core Business Layer
├── Medicine/
│   └── Primary focus on product management
│       ├── Direct link to EffectiveMaterial
│       ├── Stock management
│       └── Sales processing
│
├── EffectiveMaterial/
│   └── Medical information management
│       ├── Drug interactions
│       ├── Side effects
│       └── Usage guidelines
│
├── Stock/
│   └── Inventory control
│       ├── Medicine quantities
│       ├── Expiry tracking
│       └── Location management
│
└── Order/
    └── Sales operations
        ├── Customer orders
        ├── Prescriptions
        └── Transactions

Medical Reference Layer
├── Disease/
├── SideEffects/
├── Symptoms/
├── Uses/
└── Food/

Product Management Layer
├── Manufacturers/
├── DosageForm/
├── Unit/
├── Supplier/
├── SupplierInvoice/
└── SupplierTransaction/

Security Layer
├── Auth/
├── Identity/
└── Permissions/



Key Points about Priority:

Core Business Entities
Handle critical business operations
Most frequently accessed
Require highest performance optimization
Need strongest data validation


Medical Reference Entities

Support clinical decision making
Crucial for patient safety
Used in validation rules
Important for regulatory compliance


Product Management Entities

Support inventory operations
Critical for supply chain
Important for business operations
Handle supplier relationships


Security Entities

Ensure system security
Manage user access
Handle authentication
Control permissions