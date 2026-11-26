using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace PaaPindenBlazor.Services
{
    public interface IFileModelService
    {
        Task<string> UploadFileAsync(IFormFile formFile);
        Task UpdateFileAsync(IFormFile formFile, string filePath);
        Task<string> GetFilePathWithVersionAsync(string filePath);
        Task DeleteFileAsync(string filePath);
    }
}
