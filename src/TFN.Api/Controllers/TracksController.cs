using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Models.Entities;
using TFN.Mvc.Helpers;

namespace TFN.Api.Controllers
{
    [Route("api/tracks")]
    public class TracksController : Controller
    {
        public IHostingEnvironment Environment { get; private set; }
        public ITrackStorageService TrackStorageService { get; private set; }
        public ITrackProcessingService TrackProcessingService { get; private set; }
        public ILogger Logger { get; private set; }
        // Get the default form options so that we can use them to set the default limits for
        // request body data
        private static readonly FormOptions DefaultFormOptions = new FormOptions();

        private CloudBlobClient BlobClient;
        public TracksController(IHostingEnvironment environment, ITrackProcessingService trackProcessingService, ITrackStorageService trackStorageService, ILogger<TracksController> logger)
        {
            Environment = environment;
            TrackStorageService = trackStorageService;
            TrackProcessingService = trackProcessingService;
            Logger = logger;

            BlobClient = CloudStorageAccount.Parse("UseDevelopmentStorage=true;").CreateCloudBlobClient();

        }

        [HttpGet("{trackId:Guid}", Name = "GetTrack")]
        [Authorize("tracks.read")]
        public async Task<IActionResult> GetAsync(Guid trackId)
        {
            throw new NotImplementedException();
        }

        [HttpPost(Name = "PostTrack")]
        //[Authorize("tracks.write")]
        public async Task<IActionResult> PostAsync()
        {
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                return BadRequest($"Expected a multipart request, but got '{Request.ContentType}'.");
            }

            // Used to accumulate all the form url encoded key value pairs in the request.
            var formAccumulator = new KeyValueAccumulator();
            string targetFilePath = null;

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType), DefaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);

            var section = await reader.ReadNextSectionAsync();
            while (section != null)
            {
                ContentDispositionHeaderValue contentDisposition;
                ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

                if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                {
                    var name = HeaderUtilities.RemoveQuotes(contentDisposition.Name) ?? string.Empty;
                    var fileName = HeaderUtilities.RemoveQuotes(contentDisposition.FileName) ?? string.Empty;

                    if (name.Equals("track", StringComparison.CurrentCultureIgnoreCase))
                    {
                        var format = fileName.Split('.').Last();

                        var unprocessedFileName = $"{Guid.NewGuid()}.{format}";
                        var processedFileName = $"{Guid.NewGuid()}.mp3";
                        
                        var memoryStream = new MemoryStream();
                        
                        section.Body.CopyTo(memoryStream);
                       

                        Logger.LogInformation($"track with name [{fileName}] to be processed with format [{format}] as [{unprocessedFileName}]");

                        var unprocessedUri = await TrackStorageService.UploadUnprocessedAsync(memoryStream, unprocessedFileName);

                        Logger.LogInformation($"unprocessed track is stored at [{unprocessedUri}]");

                        Logger.LogInformation($"processing track [{unprocessedFileName}]");

                        var waveSource = await TrackProcessingService.GetWaveSourceAsync(unprocessedUri);


                        //var processedUri = await TrackStorageService.UploadProcessedAsync()

                    }

                    // Here the uploaded file is being copied to local disk but you can also for example, copy the
                    // stream directly to let's say Azure blob storage
                    //targetFilePath = Path.Combine(Environment.ContentRootPath, Guid.NewGuid().ToString());

                    

                    /*var blobContainer = BlobClient.GetContainerReference("tracks");
                    blobContainer.CreateIfNotExists();

                    var block = blobContainer.GetBlockBlobReference("unprocessed");

                    block.UploadFromStream(section.Body);*/

                    /*using (var targetStream = System.IO.File.Create(targetFilePath))
                    {
                        await section.Body.CopyToAsync(targetStream);


                        //copy to blob

                        //_logger.LogInformation($"Copied the uploaded file '{fileName}' to '{targetFilePath}'.");
                    }*/
                }
                else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
                {
                    // Content-Disposition: form-data; name="key"
                    //
                    // value

                    // Do not limit the key name length here because the mulipart headers length
                    // limit is already in effect.
                    var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
                    MediaTypeHeaderValue mediaType;
                    MediaTypeHeaderValue.TryParse(section.ContentType, out mediaType);
                    var encoding = FilterEncoding(mediaType?.Encoding);
                    using (var streamReader = new StreamReader(
                        section.Body,
                        encoding,
                        detectEncodingFromByteOrderMarks: true,
                        bufferSize: 1024,
                        leaveOpen: true))
                    {
                        // The value length limit is enforced by MultipartBodyLengthLimit
                        var value = await streamReader.ReadToEndAsync();
                        formAccumulator.Append(key, value);

                        if (formAccumulator.ValueCount > DefaultFormOptions.ValueCountLimit)
                        {
                            throw new InvalidDataException(
                                $"Form key count limit {DefaultFormOptions.ValueCountLimit} exceeded.");
                        }
                    }
                }

                // Drains any remaining section body that has not been consumed and
                // reads the headers for the next section.
                section = await reader.ReadNextSectionAsync();
            }

            

            
            return Json("ok");
        }

        private static Encoding FilterEncoding(Encoding encoding)
        {
            // UTF-7 is insecure and should not be honored. UTF-8 will succeed for most cases.
            if (encoding == null || Encoding.UTF7.Equals(encoding))
            {
                return Encoding.UTF8;
            }
            return encoding;
        }
        /*
       public bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
       {
           try
           {
               // Open file for reading
               System.IO.FileStream _FileStream =
                  new System.IO.FileStream(_FileName, System.IO.FileMode.Create,
                                           System.IO.FileAccess.Write);
               // Writes a block of bytes to this stream using data from
               // a byte array.
               _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

               // close file stream
               _FileStream.Close();

               return true;
           }
           catch (Exception _Exception)
           {
               // Error
               Console.WriteLine("Exception caught in process: {0}",
                                 _Exception.ToString());
           }

           // error occured, return false
           return false;
       }

       public static byte[] ReadToEnd(Stream stream)
       {
           long originalPosition = 0;

           if (stream.CanSeek)
           {
               originalPosition = stream.Position;
               stream.Position = 0;
           }

           try
           {
               byte[] readBuffer = new byte[4096];

               int totalBytesRead = 0;
               int bytesRead;

               while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
               {
                   totalBytesRead += bytesRead;

                   if (totalBytesRead == readBuffer.Length)
                   {
                       int nextByte = stream.ReadByte();
                       if (nextByte != -1)
                       {
                           byte[] temp = new byte[readBuffer.Length * 2];
                           Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                           Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                           readBuffer = temp;
                           totalBytesRead++;
                       }
                   }
               }

               byte[] buffer = readBuffer;
               if (readBuffer.Length != totalBytesRead)
               {
                   buffer = new byte[totalBytesRead];
                   Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
               }
               return buffer;
           }
           finally
           {
               if (stream.CanSeek)
               {
                   stream.Position = originalPosition;
               }
           }
       }*/
    }
}
