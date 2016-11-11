using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CSCore;
using CSCore.MediaFoundation;
using TFN.Domain.Interfaces.Services;
using System.Linq;
using CSCore.Codecs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TFN.Domain.Services
{
    public class TrackProcessingService : ITrackProcessingService
    {
        public ILogger Logger { get; private set; }

        public int Bitrate { get; private set; }
        public TrackProcessingService(ILogger<TrackProcessingService> logger, IConfiguration configuration)
        {
            Logger = logger;
            Bitrate = Int32.Parse(configuration["TranscodeBitrate"]);
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

                    Console.WriteLine("Samplerate {0} -> {1}", source.WaveFormat.SampleRate, sampleRate);
                    Console.WriteLine("Channels {0} -> {1}", source.WaveFormat.Channels, 2);
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

        public Task<Stream> TranscodeAudioAsync(IWaveSource track)
        {
            var supportedFormats = MediaFoundationEncoder.GetEncoderMediaTypes(AudioSubTypes.MpegLayer3);
            if (!supportedFormats.Any())
            {
                Logger.LogCritical($"The current host does not support mp3 encoding");
                throw new NotSupportedException("The current host does not support mp3 encoding");
            }

            var memoryStream = new MemoryStream();

            using (track)
            {
                using (var encoder = MediaFoundationEncoder.CreateMP3Encoder(track.WaveFormat, memoryStream))
                {
                    byte[] buffer = new byte[track.WaveFormat.BytesPerSecond];
                    int read;
                    while ((read = track.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        encoder.Write(buffer, 0, read);

                        //Console.CursorLeft = 0;
                        //Console.Write("{0:P}/{1:P}", (double)track.Position / track.Length, 1);
                    }
                }
            }

            return Task.FromResult((Stream)memoryStream);

        }

    }
}
