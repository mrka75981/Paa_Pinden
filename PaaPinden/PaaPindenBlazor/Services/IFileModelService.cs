using Microsoft.AspNetCore.Components.Forms;

namespace PaaPindenBlazor.Services
{
    public interface IFileModelService
    {
        void SetFilePath();
        Task<string> UploadFile();
        Task OnSelectedFile(InputFileChangeEventArgs args);
    }
}
