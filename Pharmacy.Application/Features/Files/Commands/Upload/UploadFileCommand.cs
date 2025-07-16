using MediatR;
using Microsoft.AspNetCore.Http;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Files.Commands.Upload;

public class UploadFileCommand : IRequest<Result<List<string>>>
{
    public required IFormFileCollection Files { get; set; }
}
