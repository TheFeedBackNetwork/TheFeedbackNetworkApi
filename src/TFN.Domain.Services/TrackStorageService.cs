using System;
using System.IO;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Components;
using TFN.Domain.Interfaces.Services;

namespace TFN.Domain.Services
{
    public class TrackStorageService : ITrackStorageService
    {
        public IBlobStorageComponent BlobStorageComponent { get; private set; }
        public IS3StorageComponent S3StorageComponent { get; private set; }
        const string UnprocessedContainer = "unprocessedtracks";
        const string ProcessedContainer = "processedtracks";
        public TrackStorageService(IBlobStorageComponent blobStorageComponent, IS3StorageComponent s3StorageComponent)
        {
            BlobStorageComponent = blobStorageComponent;
            S3StorageComponent = s3StorageComponent;

        }
        public async Task<Uri> UploadProcessedAsync(Stream trackStream, string fileName)
        {
            return await BlobStorageComponent.UploadAsync(trackStream, ProcessedContainer, fileName);
        }

        public async Task<Uri> UploadUnprocessedAsync(Stream trackStream, string fileName)
        {
            return await BlobStorageComponent.UploadAsync(trackStream, UnprocessedContainer, fileName);
        }
    }
}
