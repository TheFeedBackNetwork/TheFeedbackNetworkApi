using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CSCore;
using CSCore.MediaFoundation;
using TFN.Domain.Interfaces.Services;
using System.Linq;
using CSCore.Codecs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TFN.Domain.Services
{
    public class TrackProcessingService : ITrackProcessingService, IDisposable
    {
        public ITrackStorageService TrackStorageService { get; private set; }
        public ILogger Logger { get; private set; }

        public int Bitrate { get; private set; }
        public TrackProcessingService(ILogger<TrackProcessingService> logger, IConfiguration configuration, ITrackStorageService trackStorageService)
        {
            Environment = environment;
            Logger = logger;
            Bitrate = Int32.Parse(configuration["TranscodeBitrate"]);
            TrackStorageService = trackStorageService;
        }

        public Task<IReadOnlyList<int>> GetSoundWaveAsync(IWaveSource track)
        {
            throw new NotImplementedException();
        }

        public Task<IWaveSource> GetWaveSourceAsync(Stream trackStream)
        {
            throw new NotImplementedException();
        }

        public Task<IWaveSource> GetWaveSourceAsync(Uri trackUri)
        {
            var supportedFormats = MediaFoundationEncoder.GetEncoderMediaTypes(AudioSubTypes.MpegLayer3);
            if (!supportedFormats.Any())
            {
                Logger.LogCritical($"The current host does not support mp3 encoding");
                throw new NotSupportedException("The current host does not support mp3 encoding");
            }

            IWaveSource source;
            try
            {
                Logger.LogInformation($"getting source from [{trackUri}]");

                source = CodecFactory.Instance.GetCodec(new Uri(trackUri.AbsoluteUri));
                /*source =
                    CodecFactory.Instance.GetCodec(
                        new Uri("https://identitydeveloptstorage.blob.core.windows.net/test/test.wav"));*/
                Logger.LogInformation($"source retrieved [{source}]");

                if (
                    supportedFormats.All(
                        x => x.SampleRate != source.WaveFormat.SampleRate && x.Channels == source.WaveFormat.Channels))
                {
                    //the encoder does not support the input sample rate -> convert it to any supported samplerate
                    //choose the best sample rate with stereo (in order to make simple, we always use stereo in this sample)
                    int sampleRate =
                        supportedFormats.OrderBy(x => Math.Abs(source.WaveFormat.SampleRate - x.SampleRate))
                            .First(x => x.Channels == source.WaveFormat.Channels)
                            .SampleRate;

                    Logger.LogInformation($"Samplerate [{source.WaveFormat.SampleRate}] -> [{sampleRate}]");
                    Logger.LogInformation($"Channels [{source.WaveFormat.Channels}] -> [{2}]");

                    source = source.ChangeSampleRate(sampleRate);
                }


            }
            catch (Exception e)
            {
                Logger.LogCritical("format not supported");
                Logger.LogCritical(e.ToString());
                throw new NotSupportedException("format not supported");
            }

            return Task.FromResult(source);
            
        }

        public Task<string> TranscodeAudioAsync(IWaveSource track, string fileName)
        {
            var supportedFormats = MediaFoundationEncoder.GetEncoderMediaTypes(AudioSubTypes.MpegLayer3);
            if (!supportedFormats.Any())
            {
                Logger.LogCritical($"The current host does not support mp3 encoding");
                throw new NotSupportedException("The current host does not support mp3 encoding");
            }

            var memoryStream = new MemoryStream();
            var resultStream = new MemoryStream();
            //memoryStream.
            //return Task.FromResult((Stream) memoryStream);
            byte[] buffer = new byte[track.WaveFormat.BytesPerSecond];

            using (track)
            {
                var buffer = new byte[track.WaveFormat.BytesPerSecond];
                int read;
                using (var encoder = MediaFoundationEncoder.CreateMP3Encoder(track.WaveFormat, filePath))
                {
                    //byte[] buffer = new byte[track.WaveFormat.BytesPerSecond];
                    int read;
                    while ((read = track.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        encoder.Write(buffer, 0, read);
                    }
                    resultStream = new MemoryStream(buffer);
                    var a = TrackStorageService.UploadProcessedAsync(resultStream, "lol.mp3");
                    
                    //memoryStream.WriteTo(resultStream);

                }
                var b = TrackStorageService.UploadProcessedAsync(resultStream, "lol.mp3");

                return Task.FromResult((Stream)resultStream);

            return Task.FromResult(filePath);
        }

            

        }

    }
}
