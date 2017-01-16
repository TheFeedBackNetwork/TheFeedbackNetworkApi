using Akka.Actor;
using Microsoft.AspNetCore.Mvc;
using TFN.ActorSystem;
using TFN.ActorSystem.Actors.PostsSystem;

namespace TFN.Api.Controllers
{
    [Route("api/time")]
    public class TimeCheckController : Controller
    {
        [HttpGet]
        public IActionResult GetTime()
        {
            var time = SystemActors.PostsSystemActor.Ask<PostsSystemMessages.Time>(new PostsSystemMessages.GetTime());

            return Ok(time);
        }
    }
}