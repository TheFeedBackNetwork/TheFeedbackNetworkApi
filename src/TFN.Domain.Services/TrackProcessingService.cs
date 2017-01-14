using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Services;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TFN.MediaLibrary;
using TFN.MediaLibrary.Model;
using TFN.MediaLibrary.Options;

namespace TFN.Domain.Services
{
    public class TrackProcessingService : ITrackProcessingService
    {
        public IHostingEnvironment Environment { get; private set; }
        public ILogger Logger { get; private set; }
        public int Bitrate { get; private set; }
        public TrackProcessingService(ILogger<TrackProcessingService> logger, IConfiguration configuration, IHostingEnvironment environment)
        {
            Environment = environment;
            Logger = logger;
            Bitrate = int.Parse(configuration["TranscodeBitrate"]);
        }

        public Task TranscodeAudioAsync(string sourceFilePath, string destinationFilePath)
        {
            var options = new ConversionOptions
            {
                AudioBitRate = Bitrate
            };

            var ffmpegPath = Path.Combine(Environment.ContentRootPath, "ffmpeg.exe");

            var inputFile = new MediaFile(sourceFilePath);
            var outputFile = new MediaFile(destinationFilePath);
            var outputWf = destinationFilePath.Split('.')[0] + ".png";

            using (var engine = new Engine(ffmpegPath))
            {
                engine.Convert(inputFile,outputFile,options);
            }

            return Task.CompletedTask;
        }

        public Task<List<int>> GetWaveformAsync(Guid processedFileId, string destinationFilePath)
        {

            throw new NotImplementedException();
        }
    }
}
