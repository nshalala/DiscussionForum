using Microsoft.AspNetCore.Http;

namespace DiscussionForum.Application.Abstractions.Storages;

public interface ILocalStorageService
{
    Task<List<string>> UploadAsync(string path, IFormFileCollection files);
    Task DeleteAsync(string path);
    List<string> GetFiles(string path);
    bool HasFile(string path);
    FileStream GetImageStream(string path, string filename);
}