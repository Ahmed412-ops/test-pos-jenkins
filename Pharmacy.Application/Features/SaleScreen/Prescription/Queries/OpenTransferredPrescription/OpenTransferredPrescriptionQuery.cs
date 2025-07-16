using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SaleScreen.Prescription.Queries.OpenTransferredPrescription;

public record OpenTransferredPrescriptionQuery 
    (Guid PrescriptionId) : IRequest<Result<OpenTransferredPrescriptionResponse>>;

