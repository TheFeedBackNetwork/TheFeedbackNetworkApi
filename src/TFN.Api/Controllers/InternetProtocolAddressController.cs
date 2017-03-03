using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TFN.Api.Controllers
{
    [Route("api/ip")]
    public class InternetProtocolAddressController : AppController
    {
        [HttpGet(Name = "GetIPForCaller")]
        [Authorize("ip.read")]
        public IActionResult GetIPAddress()
        {
            var IP = HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();

            var model = new Dictionary<string,string>();
            model["IP"] = IP;

            return Json(model);
        }
    }
}