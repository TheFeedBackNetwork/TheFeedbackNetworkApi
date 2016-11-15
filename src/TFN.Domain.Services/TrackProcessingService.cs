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

namespace TFN.Domain.Services
{
    public class TrackProcessingService : ITrackProcessingService
    {
        public IHostingEnvironment Environment { get; private set; }
        public ILogger Logger { get; private set; }
        public string Bitrate { get; private set; }
        public TrackProcessingService(ILogger<TrackProcessingService> logger, IConfiguration configuration, IHostingEnvironment environment)
        {
            Environment = environment;
            Logger = logger;
            Bitrate = configuration["TranscodeBitrate"];
        }

        public Task<string> TranscodeAudioAsync(string sourceFilePath, string destinationFilePath)
        {


            var ffmpegExe = Path.Combine(Environment.ContentRootPath, "ffmpeg", "bin", "ffmpeg.exe");

            var transcodeCmd = $" -i {sourceFilePath} -ab {Bitrate} {destinationFilePath}";

            var process = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    FileName = ffmpegExe,
                    Arguments = transcodeCmd
                }
            };


            process.Start();

            var g = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            var exitCode = process.ExitCode;

            if (exitCode == 0)
            {
                return Task.FromResult(destinationFilePath);
            }

            throw new InvalidOperationException($"could not transcode the audio");
            
        }
    }
}
