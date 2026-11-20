using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace PaaPindenBlazor.Services
{
    public interface IFileModelService
    {
        Task<string> UploadFile(IFormFile formFile);
        Task UpdateFile(IFormFile formFile, string fileName);
    }
}
