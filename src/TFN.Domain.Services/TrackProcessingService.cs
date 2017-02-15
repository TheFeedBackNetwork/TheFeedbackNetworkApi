using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Services;
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
            Logger.LogInformation($"PATH {ffmpegPath}");

            var inputFile = new MediaFile(sourceFilePath);
            var outputFile = new MediaFile(destinationFilePath);

            using (var engine = new Engine(ffmpegPath,true))
            {
                engine.Convert(inputFile, outputFile, options);
            }

            return Task.CompletedTask;
        }

        public Task<List<int>> GetWaveformAsync(string sourceSoundFilePath, string destinationPngFilePath)
        {
            var input = new MediaFile(sourceSoundFilePath);
            var output = new MediaFile(destinationPngFilePath);

            var ffmpegPath = Path.Combine(Environment.ContentRootPath, "ffmpeg.exe");

            using (var engine = new Engine(ffmpegPath,true))
            {
                engine.GetWaveform(input,output);
            }

            var data = GetWaveformData(destinationPngFilePath);

            return Task.FromResult(data);
        }

        private static List<int> GetWaveformData(string filename)
        {
            var bmp = (Bitmap)Image.FromFile(filename);

            var clearColour = Color.FromArgb(0, 0, 0, 0);

            var waveform = new List<int>();

            var pic = new Bitmap(bmp.Width, bmp.Height / 2);

            pic = InitPic(pic);

            for (int i = 0; i < pic.Width; i++)
            {
                for (int j = 0; j < pic.Height; j++)
                {
                    if (bmp.GetPixel(i, j + bmp.Height / 2) == clearColour)
                    {
                        pic.SetPixel(i, j, Color.Blue);
                        waveform.Add(j);
                        j = bmp.Height;

                    }
                    else
                    {
                        pic.SetPixel(i, j, Color.Blue);
                    }

                }
            }
            
            bmp.Dispose();

            return waveform;
        }

        public static Bitmap InitPic(Bitmap bmp)
        {
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    bmp.SetPixel(i, j, Color.FromArgb(0, 0, 0, 0));
                }
            }
            return bmp;
        }
    }
}
