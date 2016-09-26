using System;
using Microsoft.AspNetCore.Mvc;
using TFN.Api.Extensions;

namespace TFN.Api.Controllers
{
    public class AppController : Controller
    {
        protected Guid UserId => HttpContext.GetUserId().Value;

        public string AbsoluteUri => HttpContext.GetAbsoluteUri();
    }
}
