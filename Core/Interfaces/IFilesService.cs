using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IFilesService
    {
        Task<string> SaveCarImage(IFormFile file);
        Task DeleteCarImage(string path);
        Task<string> EditCarImage(string oldPath, IFormFile newFile);
    }
}
