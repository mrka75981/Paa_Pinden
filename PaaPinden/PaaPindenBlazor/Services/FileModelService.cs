using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace PaaPindenBlazor.Services
{
    public class FileModelService : IFileModelService
    {
        private readonly IWebHostEnvironment _webHostEnviroment;

        private string _resultMessage = "";

        private string? _folderPath = "";
        private string? _folderName = "images";
        private string? _filePath { get; set; } = "";

        public FileModelService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnviroment = webHostEnvironment;
            _folderPath = Path.Combine(_webHostEnviroment.WebRootPath, _folderName);
        }

        private void SetFilePath(IFormFile formFile)
        {
            Directory.CreateDirectory(_folderPath);

            int imageNameNum = 1;
            string imageName = $"{imageNameNum}";
            List<string> imageNames = Directory.GetFiles(_folderPath)
                .Select(s => s.Split('.').First()) //got rid of the file extension. 
                .Select(s => s.Split('\\').Last()) //got rid of the path thereto. 
                .ToList();
            while (imageNames.Contains(imageName))
            {
                imageNameNum++;
                imageName = $"{imageNameNum}";
            }

            string fileExtension = formFile.FileName.Split('.').Last();
            _filePath = Path.Combine(_folderPath, $"{imageName}.{fileExtension}");
        }

        public async Task<string> UploadFileAsync(IFormFile formFile)
        {
            try
            {
                if (formFile is null)
                {
                    throw new Exception("File not found");
                }
                SetFilePath(formFile);


                await using var stream = new FileStream(_filePath, FileMode.Create);
                await formFile.OpenReadStream().CopyToAsync(stream);
                _resultMessage = "Upload Succesful!";
                return _filePath;
            }
            catch (Exception ex)
            {
                _resultMessage = ex.Message;
            }

            return _resultMessage;

        }

        public async Task UpdateFileAsync(IFormFile formFile, string filePath)
        {
            Directory.CreateDirectory(_folderPath);

            File.Delete(filePath);
            await using FileStream stream = new FileStream(filePath, FileMode.Create);
            await formFile.CopyToAsync(stream);
        }

        public async Task<string> GetFilePathWithVersionAsync(string filePath)
        {
            var version = File.GetLastWriteTimeUtc(filePath).Ticks;
            string fileName = filePath.Split('\\').Last();
            return $"/{_folderName}/{fileName}?v={version}";
        }

        public async Task DeleteFileAsync(string filePath)
        {
            Directory.CreateDirectory(_folderPath);
            File.Delete(filePath);
        }
    }
}
