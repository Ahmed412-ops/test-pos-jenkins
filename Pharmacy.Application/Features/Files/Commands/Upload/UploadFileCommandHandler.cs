using MediatR;
using Pharmacy.Application.Services.Abstraction.FileHandler;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Files.Commands.Upload;

public class UploadFileCommandHandler(
    IFileHandler fileHandler
) : IRequestHandler<UploadFileCommand, Result<List<string>>>
{
    private readonly IFileHandler _fileHandler = fileHandler;

    public async Task<Result<List<string>>> Handle(UploadFileCommand command, CancellationToken cancellationToken)
    {
        var filePaths = new List<string>();
        string folderName = "TempFiles";

        foreach (var file in command.Files)
        {
            var filePath = await _fileHandler.SaveFile(file, folderName);
            filePaths.Add(filePath);
        }

        return Result<List<string>>.Success(filePaths);
    }
}
