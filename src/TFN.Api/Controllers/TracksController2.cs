using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using TFN.ActorSystem;
using TFN.ActorSystem.Actors.User;
using TFN.Api.Models.ResponseModels;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Controllers
{
    [Route("api/tracks2")]
    public class TracksController2 : AppController
    {
        public IHostingEnvironment Environment { get; private set; }
        public IAuthorizationService AuthorizationService { get; private set; }
        public IConfiguration Configuration { get; private set; }
        public ITrackStorageService TrackStorageService { get; private set; }
        public ITrackProcessingService TrackProcessingService { get; private set; }
        public ITrackRepository TrackRepository { get; private set; }
        public ILogger Logger { get; private set; }
        // Get the default form options so that we can use them to set the default limits for
        // request body data
        private static readonly FormOptions DefaultFormOptions = new FormOptions();

        public TracksController2(IHostingEnvironment environment, IAuthorizationService authorizationService, ITrackProcessingService trackProcessingService,
            ITrackStorageService trackStorageService, ILogger<TracksController> logger, IConfiguration configuration,
            ITrackRepository trackRepository)
        {
            Environment = environment;
            AuthorizationService = authorizationService;
            Configuration = configuration;
            TrackStorageService = trackStorageService;
            TrackProcessingService = trackProcessingService;
            TrackRepository = trackRepository;
            Logger = logger;

        }

        [HttpGet("{trackId:Guid}", Name = "GetTrack2")]
        [Authorize("tracks.read")]
        public async Task<IActionResult> GetAsync(Guid trackId)
        {
            var track = await TrackRepository.GetAsync(trackId);

            if (track == null)
            {
                return NotFound();
            }

            var model = TrackResponseModel.From(track, AbsoluteUri);

            return Json(model);
        }

        [HttpPost(Name = "PostTrack2")]
        //[Authorize("tracks.write")]
        public async Task<IActionResult> PostAsync(IList<IFormFile> files)
        {
            var file = files.FirstOrDefault();
            long totalBytes = file.Length;

            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;

            fileName = HeaderUtilities.RemoveQuotes(fileName);

            var supportedTypes = Configuration["SupportedMedia"].Split(' ');
            var format = fileName.Split('.').Last();

            if (supportedTypes.All(x => x != format))
            {
                return BadRequest($"Expected media types {supportedTypes} but got '{format}'.");
            }


            var unprocessedFileName = $"{Guid.NewGuid()}.{format}";
            var unprocessedFilePath = Path.Combine(Environment.WebRootPath, "unprocessedtracks", unprocessedFileName);

            var resourceId = Guid.NewGuid();
            var processedFileName = $"{resourceId}.mp3";
            var waveformFilename = $"{resourceId}.png";
            var processedFilePath = Path.Combine(Environment.WebRootPath, "processedtracks", processedFileName);
            var waveformFilePath = Path.Combine(Environment.WebRootPath, "processedwaveforms", waveformFilename);

            byte[] buffer = new byte[16 * 1024];

            using (var output = System.IO.File.Create(unprocessedFilePath))
            {
                using (var input = file.OpenReadStream())
                {
                    long totalReadBytes = 0;
                    int readBytes;

                    while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        var progress = (int) ((float) totalReadBytes / (float) totalBytes * 100.0);
                        var uploadProgress = new UserMessages.UploadProgress(UserId,progress);
                        SystemActors.SignalRBridgeActor.Tell(uploadProgress,ActorRefs.NoSender);

                    }
                }
            }

            Logger.LogInformation($"track with name [{fileName}] uploaded with [{format}] as [{unprocessedFileName}]");
            Logger.LogInformation($"track with name [{fileName}] uploaded to path [{unprocessedFilePath}]");

            Logger.LogInformation($"track with name [{fileName}] to be processed with format [{format}] as [{unprocessedFileName}]");


            Logger.LogInformation($"{DateTime.UtcNow} processing track [{unprocessedFileName}]");


            await TrackProcessingService.TranscodeAudioAsync(unprocessedFilePath, processedFilePath);

            var processProgress = new UserMessages.ProcessProgress(UserId,33);
            SystemActors.SignalRBridgeActor.Tell(processProgress,ActorRefs.NoSender);

            var waveFormData = await TrackProcessingService.GetWaveformAsync(processedFilePath, waveformFilePath);

            processProgress = new UserMessages.ProcessProgress(UserId, 66);
            SystemActors.SignalRBridgeActor.Tell(processProgress, ActorRefs.NoSender);

            Logger.LogInformation($"{DateTime.UtcNow} processed track with name [{processedFileName}] to be stored in storage.");

            var processedUri =
                await TrackStorageService.UploadProcessedAsync(processedFilePath, processedFileName);

            processProgress = new UserMessages.ProcessProgress(UserId, 80);

            Logger.LogInformation($"processed track is stored at [{processedUri}]");


            Logger.LogInformation("deleting processed and unprocessed tracks in wwwroot.");

            await TrackStorageService.DeleteLocalAsync(unprocessedFilePath);

            processProgress = new UserMessages.ProcessProgress(UserId, 90);
            SystemActors.SignalRBridgeActor.Tell(processProgress, ActorRefs.NoSender);

            await TrackStorageService.DeleteLocalAsync(processedFilePath);
            await TrackStorageService.DeleteLocalAsync(waveformFilePath);

            var track = new Track(resourceId, UserId, processedUri, waveFormData, DateTime.UtcNow);

            await TrackRepository.AddAsync(track);

            processProgress = new UserMessages.ProcessProgress(UserId, 100);
            SystemActors.SignalRBridgeActor.Tell(processProgress, ActorRefs.NoSender);

            var model = TrackResponseModel.From(track, AbsoluteUri);

            return CreatedAtAction("GetTrack2", new { trackId = model.Id }, model);
        }
        

    }
}