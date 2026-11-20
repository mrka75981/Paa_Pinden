using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace PaaPindenBlazor.Services
{
    public class FileModelService : IFileModelService
    {
        private readonly IWebHostEnvironment _webHostEnviroment;

        private string _resultMessage = "";

        private string? _folderPath = "";
        private string? _filePath { get; set; } = "";

        public FileModelService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnviroment = webHostEnvironment;
            _folderPath = Path.Combine(_webHostEnviroment.ContentRootPath, "KnittingPostPictures");
        }

        public void SetFilePath(IFormFile formFile)
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

        public async Task<string> UploadFile(IFormFile formFile)
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

        public async Task UpdateFile(IFormFile formFile, string fileName)
        {
            Directory.CreateDirectory(_folderPath);

            _filePath = Path.Combine(_folderPath, fileName);

            File.Delete(_filePath);
            await using FileStream stream = new FileStream(_filePath, FileMode.Create);
            await formFile.CopyToAsync(stream);
        }
    }
}
