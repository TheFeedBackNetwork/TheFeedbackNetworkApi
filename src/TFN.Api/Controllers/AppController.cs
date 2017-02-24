using System;
using Microsoft.AspNetCore.Mvc;
using TFN.Api.Extensions;
using TFN.Mvc.Models;

namespace TFN.Api.Controllers
{
    public class AppController : Controller
    {
        protected Guid UserId => HttpContext.GetUserId().Value;
        protected string Username => HttpContext.GetUsername();
        protected string AbsoluteUri => HttpContext.GetAbsoluteUri();
        protected PrincipleType Caller => HttpContext.GetCaller();
    }
}
