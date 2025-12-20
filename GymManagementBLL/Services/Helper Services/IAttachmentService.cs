using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Helper_Services
{
    public interface IAttachmentService
    {
        public string? Upload(string folderName, IFormFile file);
        public bool Delete(string folderName, string fileName);
    }
}
