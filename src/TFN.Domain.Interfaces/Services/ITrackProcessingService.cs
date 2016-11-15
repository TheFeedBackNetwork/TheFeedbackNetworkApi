using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CSCore;

namespace TFN.Domain.Interfaces.Services
{
    public interface ITrackProcessingService
    {
        Task<IWaveSource> GetWaveSourceAsync(Uri trackUri);
        Task<IWaveSource> GetWaveSourceAsync(Stream trackStream);
        Task<IReadOnlyList<int>> GetSoundWaveAsync(IWaveSource track);
        Task<Stream> TranscodeAudioAsync(IWaveSource track);
        Task<string> TranscodeAudioAsync(IWaveSource track, string fileName);

    }
}
