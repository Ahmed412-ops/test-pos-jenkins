using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Features.Return.Commands.Create;
using Pharmacy.Application.Features.SaleScreen.Medicine.Queries.GetMedicineSearch;
using Pharmacy.Application.Features.SaleScreen.Prescription.Commands.AddTransaction;
using Pharmacy.Application.Features.SaleScreen.Prescription.Commands.AutoPayment;
using Pharmacy.Application.Features.SaleScreen.Prescription.Commands.Create;
using Pharmacy.Application.Features.SaleScreen.Prescription.Commands.Update;
using Pharmacy.Application.Features.SaleScreen.Prescription.Queries.CheckPrescriptionConflict;
using Pharmacy.Application.Features.SaleScreen.Prescription.Queries.GetAll;
using Pharmacy.Application.Features.SaleScreen.Prescription.Queries.GetById;
using Pharmacy.Application.Features.SaleScreen.Prescription.Queries.OpenTransferredPrescription;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[AllowAnonymous]
public class PrescriptionsController(ISender sender) : BaseApiController
{
    private readonly ISender _mediator = sender;
    [HttpPost("CreatePrescription")]
    public async Task<ActionResult<Result<GetPrescriptionResponse>>> CreatePrescription([FromBody] CreatePrescriptionCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("GetPrescriptions")]
    public async Task<ActionResult<Result<PaginationResponse<GetPrescriptionsResponse>>>> GetPrescriptions([FromQuery] GetPrescriptionsQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("GetPrescription")]
    public async Task<ActionResult<Result<GetPrescriptionResponse>>> GetPrescription([FromQuery] GetPrescriptionQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpPost("AddTransactionManually")]
    public async Task<ActionResult<Result<AddPrescriptionTransactionResponse>>> AddTransactionManually([FromBody] AddPrescriptionTransactionCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpPost("AutoPayment")]
    public async Task<ActionResult<Result<AutoPaymentResponse>>> AutoPayment([FromBody] AutoPaymentCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpPost("CheckPrescriptionConflict")]
    public async Task<ActionResult<Result<CheckPrescriptionConflictResponse>>> CheckPrescriptionConflict([FromBody] CheckMedicineConflictQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("SearchMedicine")]
    public async Task<ActionResult<Result<PaginationResponse<GetMedicineSearchResponse>>>> GetMedicineSearch([FromQuery] GetMedicineSearchQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("OpenTransferredPrescription")]
    public async Task<ActionResult<Result<OpenTransferredPrescriptionResponse>>> OpenTransferredPrescription
    ([FromQuery] OpenTransferredPrescriptionQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpPut("UpdatePrescription")]
    public async Task<ActionResult<Result<GetPrescriptionResponse>>> UpdatePrescription
    ([FromBody] UpdatePrescriptionCommand query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpPut("Return")]
    public async Task<ActionResult<Result<CreateReturnResponse>>> Return([FromBody] CreateReturnCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
}
