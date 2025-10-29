using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.Services.AttachmentService
{
   public interface IAttachmentService
    {
        string? Upload(string FolderName, IFormFile file);

        bool Delete(string FolderName, string FileName);
    }
}
