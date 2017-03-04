using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TFN.Api.Extensions;
using TFN.Api.Models.ModelBinders;
using TFN.Api.Models.ResponseModels;
using TFN.Domain.Interfaces.Services;

namespace TFN.Api.Controllers
{
    //actually a good candidate for websockets...
    [Route("api/users")]
    public class UsersController : AppController
    {
        public IUserService UserService { get; private set; }
        public UsersController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpGet(Name = "SearchUsers")]
        [Authorize("users.read")]
        public async Task<IActionResult> SearchUsers(
            [ModelBinder(BinderType = typeof(UsernameQueryModelBinder))] string username,
            [ModelBinder(BinderType = typeof(OffsetQueryModelBinder))]short offset = 0,
            [ModelBinder(BinderType = typeof(LimitQueryModelBinder))]short limit = 7)
        {
            
            var users = await UserService.SearchUsers(username, offset, limit);

            var model = users.Select(x => CreditsResponseModel.From(x, HttpContext.GetAbsoluteUri()));

            return Json(model);
        }

        [HttpGet("me",Name = "SearchMe")]
        [Authorize("users.read")]
        public async Task<IActionResult> SearchMe()
        {
            var user = await UserService.GetByUsernameAsync(HttpContext.GetUsername());
            var credit = await UserService.GetCredits(HttpContext.GetUsername());

            if (user == null || credit == null)
            {
                return NotFound();
            }

            var model = UserResponseModel.From(user, credit, AbsoluteUri);
            
            return Json(model);
        }
    }
}