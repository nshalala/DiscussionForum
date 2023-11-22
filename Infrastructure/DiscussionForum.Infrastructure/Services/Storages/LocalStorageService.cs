using DiscussionForum.Application.Abstractions.Storages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace DiscussionForum.Infrastructure.Services.Storages;

public class LocalStorageService : ILocalStorageService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public LocalStorageService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<List<string>> UploadAsync(string path, IFormFileCollection files)
    {
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        List<string> datas = new();
        foreach (IFormFile file in files)
        {
            string fileNewName = FileRenameAsync(file.FileName);
            await using var stream = new FileStream($"{uploadPath}/{fileNewName}", FileMode.Create);
            await file.CopyToAsync(stream);
            await stream.FlushAsync();
            datas.Add($"{uploadPath}/{fileNewName}");
        }

        return datas;
    }

    private string FileRenameAsync(string name)
    {
        string fileExtension = Path.GetExtension(name);
        return $"image-{Guid.NewGuid()}{fileExtension}";
    }

    public Task DeleteAsync(string path)
    {
        File.Delete($"{path}");
        return Task.CompletedTask;
    }

    public List<string> GetFiles(string path)
    {
        DirectoryInfo directory = new(path);
        return directory.GetFiles().Select(f => f.Name).ToList();
    }

    public bool HasFile(string path)
        => File.Exists($"{path}");

    public FileStream GetImageStream(string path, string filename)
    {
        string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, path, filename);
        return new FileStream(imagePath, FileMode.Open);
    }
}