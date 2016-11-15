using System;
using System.IO;
using System.Threading.Tasks;

namespace TFN.Domain.Interfaces.Services
{
    public interface ITrackStorageService
    {
        Task<Uri> UploadUnprocessedAsync(Stream trackStream, string fileName);
        Task<Uri> UploadProcessedAsync(Stream trackStream, string fileName);
        Task<Uri> UploadProcessedAsync(string path, string fileName);
        Task DeleteLocalAsync(string filePath);
    }
}
