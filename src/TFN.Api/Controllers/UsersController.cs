using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TFN.Api.Extensions;
using TFN.Api.Models.ModelBinders;
using TFN.Api.Models.ResponseModels;
using TFN.Domain.Interfaces.Services;

namespace TFN.Api.Controllers
{
    //actually a good candidate for websockets...
    [Route("api/users")]
    public class UsersController : Controller
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
    }
}