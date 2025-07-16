using MediatR;
using Microsoft.AspNetCore.Http;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Symptom.Commands.Delete;

public class DeleteSymptomCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}
