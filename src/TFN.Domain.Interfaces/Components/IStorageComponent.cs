using System;
using System.IO;
using System.Threading.Tasks;

namespace TFN.Domain.Interfaces.Components
{
    public interface IStorageComponent
    {
        Task<Uri> UploadAsync(Stream trackStream, string container, string fileName);
        Task DeleteAsync(string container, string fileName);
    }
}
