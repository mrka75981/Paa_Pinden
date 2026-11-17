using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace PaaPindenBlazor.Services
{
    public class FileModelService : IFileModelService
    {
        private readonly IWebHostEnvironment _webHostEnviroment;

        private string _resultmessage = "";

        private string? _folderpath = "";
        private string? _filepath { get; set; } = "";

        public FileModelService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnviroment = webHostEnvironment;
        }

        public void SetFilePath(IFormFile formFile)
        {
            _folderpath = Path.Combine(_webHostEnviroment.ContentRootPath, "KnittingPostPictures");
            Directory.CreateDirectory(_folderpath);

            int imageNameNum = 1;
            string imageName = $"{imageNameNum}";
            List<string> imageNames = Directory.GetFiles(_folderpath)
                .Select(s => s.Split('.').First()) //got rid of the file extension. 
                .Select(s => s.Split('\\').Last()) //got rid of the path thereto. 
                .ToList();
            while (imageNames.Contains(imageName))
            {
                imageNameNum++;
                imageName = $"{imageNameNum}";
            }

            string fileExtension = formFile.FileName.Split('.').Last();
            _filepath = Path.Combine(_folderpath, $"{imageName}.{fileExtension}");


            
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


                await using var stream = new FileStream(_filepath, FileMode.Create);
                await formFile.OpenReadStream().CopyToAsync(stream);
                _resultmessage = "Upload Succesful!";
                return _filepath;
            }
            catch (Exception ex)
            {
                _resultmessage = ex.Message;
            }

            return _resultmessage;

        }
        
    }
}
