using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace PaaPindenBlazor.Services
{
    public class FileModelService : IFileModelService
    {
        private readonly IWebHostEnvironment _webHostEnviroment;
        private IBrowserFile? _file { get; set; }

        private string _resultmessage = "";

        private string? _folderpath = "";
        private string? _filepath { get; set; } = "";

        public FileModelService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnviroment = webHostEnvironment;
        }

        public void SetFilePath()
        {
            _folderpath = Path.Combine(_webHostEnviroment.ContentRootPath, "KnittingPostPictures");
            Directory.CreateDirectory(_folderpath);

            _filepath = Path.Combine(_folderpath, Path.GetFileName(_file!.Name));

            
        }

        public async Task<string> UploadFile()
        {
            try
            {
                if (_file is null)
                {
                    throw new Exception("File not found");
                }
                SetFilePath();


                await using var stream = new FileStream(_filepath, FileMode.Create);
                await _file.OpenReadStream().CopyToAsync(stream);
                _resultmessage = "Upload Succesful!";
                return _filepath;
            }
            catch (Exception ex)
            {
                _resultmessage = ex.Message;
            }

            return _resultmessage;

        }
        public async Task OnSelectedFile(InputFileChangeEventArgs args)
        {
            _file = args.File;
        }
    }
}
