using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Pharmacy.Application.Services.Abstraction.FileHandler;

namespace Pharmacy.Application.Services.Implementation;

public class FileHandler(IWebHostEnvironment env) : IFileHandler
{
    private readonly IWebHostEnvironment _env = env;

    public async Task<string> SaveFile(IFormFile file, string folder)
    {
        var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
        var directory = Path.Combine(_env.WebRootPath, "Files", folder);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        var path = Path.Combine(_env.WebRootPath, "Files", folder, fileName);
        using (var fileStream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
            return $"Files/{folder}/{fileName}";
        }
    }

    public bool DeleteFile(string path, string folder)
    {
        var rootpath = $"{_env.WebRootPath}\\Files\\{folder}";
        var files = Directory.GetFiles(rootpath);

        if (files.Length > 0)
        {
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                fileInfo.Delete();
            }
            return true;
        }

        return false;
    }

    public bool DeleteFile(string path)
    {
        var _path = $"{_env.WebRootPath}\\{path}";

        if (!File.Exists(_path))
            return false;

        File.Delete(_path);
        return true;
    }

    public async Task<string?> MoveFile(string? filePath, string folder)
    {
        if (
            !string.IsNullOrWhiteSpace(filePath)
            && filePath.Contains("TempFiles")
            && await FileExists(filePath)
        )
        {
            var fileName = Path.GetFileName(filePath);
            var rootpath = $"{_env.WebRootPath}";
            var oldPath = $"{rootpath}\\{filePath}";
            var newPath = $"{rootpath}\\Files\\{folder}\\{fileName}";

            if (!Directory.Exists($"{rootpath}\\Files\\{folder}"))
                Directory.CreateDirectory($"{rootpath}\\Files\\{folder}");

            await Task.Factory.StartNew(() =>
            {
                File.Move(oldPath, newPath);
            });

            return $"Files\\{folder}\\{fileName}";
        }

        return filePath;
    }

    public async Task<string?> UpdateFile(string oldPath, string tempPath, string folder)
    {
        if (oldPath == tempPath)
            return oldPath;

        var newPath = await MoveFile(tempPath, folder);
        if (oldPath != null)
            DeleteFile(oldPath);

        return newPath;
    }

    public async Task<bool> FileExists(string path)
    {
        var _path = $"{_env.WebRootPath}\\{path}";
        return await Task.FromResult(File.Exists(_path));
    }
}
