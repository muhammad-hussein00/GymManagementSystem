using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Helper_Services
{
    public class AttachmentService : IAttachmentService
    {


        private readonly string[] AllowedExtentions = { ".jpg", ".png", ".jpeg" };
        private readonly long MaxFileSize = 5 * 1024 * 1024;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AttachmentService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string? Upload(string folderName, IFormFile file)
        {
            try
            {
                if (folderName is null || file is null || file.Length == 0)
                    return null;

                if (file.Length > MaxFileSize)
                    return null;

                var extention = Path.GetExtension(file.FileName).ToLower();

                if (!AllowedExtentions.Contains(extention))
                    return null;

                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", folderName);

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fileName = Guid.NewGuid().ToString() + extention;

                var filePath = Path.Combine(folderPath, fileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);

                file.CopyTo(fileStream);

                return fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to upload photo. {ex}");
                return null;
            }
        }
        public bool Delete(string folderName, string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(folderName) || string.IsNullOrEmpty(fileName))
                    return false;

                var fullFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", folderName, fileName);

                if (File.Exists(fullFilePath))
                {
                    File.Delete(fullFilePath);
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete file. {ex}");
                return false;
            }
        }

    }
}
