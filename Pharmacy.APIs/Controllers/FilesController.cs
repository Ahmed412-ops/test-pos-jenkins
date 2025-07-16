using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Features.Files.Commands.Upload;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;
[Authorize]
public class FilesController(ISender mediator) : BaseApiController
{
    private readonly ISender _mediator = mediator;

    /// <summary>
    /// Uploads multiple files to the specified folder.
    /// </summary>
    /// <param name="files">Collection of files to be uploaded.</param>
    /// <param name="folderName">Folder name where the files will be saved.</param>
    /// <returns>List of file paths.</returns>
    [HttpPost("upload")]
    public async Task<ActionResult<Result<List<string>>>> UploadFiles([FromForm] UploadFileCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}
