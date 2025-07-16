using Microsoft.AspNetCore.Http;

namespace Pharmacy.Application.Services.Abstraction.FileHandler;

public interface IFileHandler
{
    Task<string> SaveFile(IFormFile file, string folder);
    public bool DeleteFile(string path, string folder);
    public bool DeleteFile(string path);
    Task<string?> MoveFile(string? filePath, string folder);
    Task<bool> FileExists(string path);
    Task<string?> UpdateFile(string oldPath, string tempPath, string folder);
}
