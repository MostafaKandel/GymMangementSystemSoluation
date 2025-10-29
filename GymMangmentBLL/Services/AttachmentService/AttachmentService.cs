using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        private readonly string[] AllowedExtentions= {".jpg", ".png", ".jpeg"};
        private readonly long MaxFileSize = 5 * 1024 * 1024; // 5 MB
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AttachmentService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string? Upload(string FolderName, IFormFile file)
        {
            try {
                if (FolderName is null || file is null || file.Length == 0) return null;
                if (file.Length > MaxFileSize) return null;
                var Extension = Path.GetExtension(file.FileName).ToLower();
                if (!AllowedExtentions.Contains(Extension)) return null;

                var FolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", FolderName);
                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }
                var FileName = Guid.NewGuid().ToString() + Extension;
                var FilePath = Path.Combine(FolderPath, FileName);
                using var stream = new FileStream(FilePath, FileMode.Create);
                file.CopyTo(stream);

                return FileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed To Upload File To Folder: {FolderName}: {ex}");
                return null;
            }


        }
        public bool Delete(string FileName,string FolderName)
        {
            try
            {
                if(string.IsNullOrEmpty(FolderName) || string.IsNullOrEmpty(FileName)) 
                    return false;
                var FilePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", FolderName, FileName);
                if (File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed To Delete File: {FileName} From Folder: {FolderName}: {ex}");
                return false;
            }

        }
    }
}
